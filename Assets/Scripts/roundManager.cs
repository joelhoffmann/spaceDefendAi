using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    public GameObject[] spawnPoints; //Spawnpoints welche die enemy directions bestimmen
    public List<Transform> inactiveSpawnPoints = new List<Transform>();
    public List<Transform> activeSpawnPoints = new List<Transform>();


    private Transform container;
    public GameObject enemyPrefab; // Das Feind-Prefab
    private int currentRound = 0; // Aktuelle Runde
    private List<GameObject> currentEnemies = new List<GameObject>() ; // Liste der aktuellen Feinde
    private bool lockRound = false;
    private int enemiesPerWave = 0;

    //UI Result Screen
    VisualElement root;
    public UIDocument ResultScreen;
    private UIDocument resultScreenInstance;
    Button restartButton;

    public static RoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RoundManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("RoundManager");
                    instance = managerObject.AddComponent<RoundManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        Debug.Log("RESULT SCREEN:" + root);
        resultScreenInstance = Instantiate(ResultScreen).GetComponent<UIDocument>();
        root = resultScreenInstance.rootVisualElement;
        root.style.display = DisplayStyle.None;
        



        container = GameObject.Find("activeEnemyContainer").transform;
        //enemyPrefab = Resources.Load<GameObject>("enemyTemplate"); // Setze das Prefab aus den Ressourcen
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab not found! Make sure to place the prefab in a Resources folder.");
        }

        if (spawnPoints.Length > 0)
        {
            inactiveSpawnPoints = new List<Transform>();
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                inactiveSpawnPoints.Add(spawnPoints[i].transform);
            }

            //    Debug.Log("initiated spawnpints");
            //    Debug.Log("inActiveSpawnPoints: " + inactiveSpawnPoints.Count);
        }
        else
        {
            Debug.LogError("No spawn points found! Make sure to place the spawn points in the scene.");
        }

        StartNewRound();
    }

    void Start()
    {
        Player.instance.OnPlayerDeath += HandlePlayerDeath;
        container = GameObject.Find("activeEnemyContainer").transform;

        //StartNewRound();
    }

    void StartNewRound()
    {
        // Debug.Log("Starting Round " + currentRound);
        GameObject.Find("roundDisplay").GetComponent<TextMeshProUGUI>().text = "Round " + currentRound.ToString();
        lockRound = false;
        CoinManager.Instance.AddCoins(1000);

        if (currentRound == 1 || (currentRound % 5 == 0 && activeSpawnPoints.Count <= spawnPoints.Length && currentRound < 20))
        {
            addNewActiveSpawnPoint();
        }


        // Spawnen Sie die Feinde f�r die aktuelle Runde
        enemiesPerWave = (int)(0.5 * currentRound + 1);
        // print("enemiesPerWave: " + enemiesPerWave);

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
        }
    }


    void SpawnEnemy()
    {
        // W�hlen Sie eine zuf�llige Position um den Bildschirmrand aus
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();

        GameObject newEnemyObject = Instantiate(enemyPrefab, container);

        // Kopieren Sie die Position des Spawnpunkts, um sie zu ändern
        Vector3 adjustedSpawnPosition = randomSpawnPosition;
        // Fügen Sie die Variation zur Position des Spawnpunkts hinzu
        adjustedSpawnPosition += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

        newEnemyObject.transform.position = adjustedSpawnPosition;
        newEnemyObject.SetActive(true);

        currentEnemies.Add(newEnemyObject);
    }

    Vector3 GetRandomSpawnPosition()
    {
    
        //get random spawnpoint from active spawnpoints
        // Debug.Log("activeSpawnPoints: " + activeSpawnPoints.Count);
        int randomIndex = Random.Range(0, activeSpawnPoints.Count);
        Transform randomSpawnPoint = activeSpawnPoints[randomIndex];
        // add varition from spawPoint direction +/- 1f
        Vector3 randomSpawnPosition = randomSpawnPoint.position;
        Vector3 adjustedSpawnPosition = randomSpawnPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        return adjustedSpawnPosition;
        ;
    }

    public void DecreaseEnemyCount(GameObject enemy)
    {
        //   Debug.Log("----------------sdffghgd-------------------");
        currentEnemies.Remove(enemy);
    }

    private void addNewActiveSpawnPoint()
    {
        bool newSpawnPointFound = false;
        while (newSpawnPointFound == false)
        {
            //get random spawnpoint from inactive spawnpoints
            int randomIndex = Random.Range(0, inactiveSpawnPoints.Count);
            Transform newRandomSpawnPoint = inactiveSpawnPoints[randomIndex];
            //check if random spawnpoint is already in active spawnpoints
            if (!activeSpawnPoints.Contains(newRandomSpawnPoint))
            {
                //add random spawnpoint to active spawnpoints
                activeSpawnPoints.Add(newRandomSpawnPoint);
                newSpawnPointFound = true;
            }
        }
    }

    private void HandlePlayerDeath()
    {
        StopEnemySpawn();
        ShowRunEndingScreen();
    }

    private void StopEnemySpawn()
    {

    }

    private void ShowRunEndingScreen()
    {
        Debug.Log("PLAYER DEATH EVENT TRIGGERED");
        root.style.display = DisplayStyle.Flex;
        restartButton = root.Query<Button>("restart");
        restartButton.clickable.clicked += OnRestartButtonClick;
    }

    void OnRestartButtonClick()
    {
        // Handle the button click event here
        Debug.Log("Restart button clicked!");
    }


    void Update()
    {
        // �berpr�fen Sie, ob alle Feinde besiegt wurden
        if (currentEnemies.Count == 0 && lockRound == false)
        {
            currentRound++; // Erh�hen Sie die Rundennummer
            lockRound = true;           
            Player.instance.ReceiveCoins(150);
            Invoke("StartNewRound", 3);
            //StartNewRound(); // Starten Sie die n�chste Runde
        }
        if (Player.instance.health <= 0 && lockRound == false)
        {
            Debug.Log("Game Over!");
            // Implementieren Sie hier die Logik f�r das Spielende
            currentRound = 1;
            lockRound = true;
            Player.instance.Respawn();
            Invoke("StartNewRound", 3);
        }

    }
}
