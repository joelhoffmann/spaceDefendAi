using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    public float spawnInterval = 2f; // Intervall fuer das Spawnen von Feinden
    public int startEnemyCount = 3; // Anzahl der Feinde zu Beginn
    public int enemyIncreasePerRound = 1; // Anzahl der zusaetzlichen Feinde pro Runde
    public GameObject[] spawnPoints; //Spawnpoints welche die enemy directions bestimmen
    public List<Transform> inActiveSpawnPoints = new List<Transform>();
    public List<Transform> activeSpawnPoints = new List<Transform>();


    private Transform container;
    private Transform enemyTemplate;
    private GameObject enemyPrefab; // Das Feind-Prefab
    private int currentRound = 1; // Aktuelle Runde
    private List<GameObject> currentEnemies = new List<GameObject>(); // Liste der aktuellen Feinde
    private bool lockRound = false;
    private int enemiesPerWave = 0;


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

        container = GameObject.Find("activeEnemyContainer").transform;
        enemyPrefab = Resources.Load<GameObject>("enemyTemplate"); // Setze das Prefab aus den Ressourcen
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab not found! Make sure to place the prefab in a Resources folder.");
        }

        if (spawnPoints.Length > 0)
        {
            inActiveSpawnPoints = new List<Transform>();
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                inActiveSpawnPoints.Add(spawnPoints[i].transform);
            }

            Debug.Log("initiated spawnpints");
            Debug.Log("inActiveSpawnPoints: " + inActiveSpawnPoints.Count);
        }

        else
        {
            Debug.LogError("No spawn points found! Make sure to place the spawn points in the scene.");
        }

        StartNewRound();
    }

    void Start()
    {
        container = GameObject.Find("activeEnemyContainer").transform;
        //StartNewRound();
    }

    void StartNewRound()
    {
        Debug.Log("Starting Round " + currentRound);
        GameObject.Find("roundDisplay").GetComponent<TextMeshProUGUI>().text = "Round " + currentRound.ToString();
        lockRound = false;
        CoinManager.Instance.AddCoins(1000);

        if (currentRound == 1 || (currentRound % 5 == 0 && activeSpawnPoints.Count <= spawnPoints.Length && currentRound < 20 ))
        {
            addNewActiveSpawnPoint();
        }


        // Spawnen Sie die Feinde f�r die aktuelle Runde
        enemiesPerWave = (int)(0.5 * currentRound + 1);
        print("enemiesPerWave: " + enemiesPerWave);

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
        /*
        float screenWidth = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        float screenHeight = Camera.main.orthographicSize * 2f;

        // W�hlen Sie eine zuf�llige Seite des Bildschirms aus
        int side = Random.Range(0, 4); // 0: oben, 1: rechts, 2: unten, 3: links

        // W�hlen Sie eine zuf�llige Position auf der ausgew�hlten Seite aus
        switch (side)
        {
            case 0: // oben
                return new Vector3(Random.Range(-screenWidth / 2f, screenWidth / 2f), screenHeight / 2f + 1f, 0f);
            case 1: // rechts
                return new Vector3(screenWidth / 2f + 1f, Random.Range(-screenHeight / 2f, screenHeight / 2f), 0f);
            case 2: // unten
                return new Vector3(Random.Range(-screenWidth / 2f, screenWidth / 2f), -screenHeight / 2f - 1f, 0f);
            case 3: // links
                return new Vector3(-screenWidth / 2f - 1f, Random.Range(-screenHeight / 2f, screenHeight / 2f), 0f);
            default:
                return Vector3.zero;
        }
        */
        //get random spawnpoint from active spawnpoints
        Debug.Log("activeSpawnPoints: " + activeSpawnPoints.Count);
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
        Debug.Log("----------------sdffghgd-------------------");
        currentEnemies.Remove(enemy);
    }

    private void addNewActiveSpawnPoint()
    {
        bool newSpawnPointFound = false;
        while (newSpawnPointFound == false)
        {
            //get random spawnpoint from inactive spawnpoints
            int randomIndex = Random.Range(0, inActiveSpawnPoints.Count);
            Transform newRandomSpawnPoint = inActiveSpawnPoints[randomIndex];
            //check if random spawnpoint is already in active spawnpoints
            if (!activeSpawnPoints.Contains(newRandomSpawnPoint))
            {
                //add random spawnpoint to active spawnpoints
                activeSpawnPoints.Add(newRandomSpawnPoint);
                newSpawnPointFound = true;
            }
        }
    }


    void Update()
    {
        // �berpr�fen Sie, ob alle Feinde besiegt wurden
        if (currentEnemies.Count == 0 && lockRound == false)
        {
            currentRound++; // Erh�hen Sie die Rundennummer
            lockRound = true;
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

    /*
        private void addNewActiveSpawnPoint()
        {
            //get random spawnpoint from inactive spawnpoints
            int randomIndex = Random.Range(0, inActiveSpawnPoints.Length);
            Transform randomSpawnPoint = inActiveSpawnPoints[randomIndex];
            //add random spawnpoint to active spawnpoints
            activeSpawnPoints.Add(randomSpawnPoint);
            //remove random spawnpoint from inactive spawnpoints
            inActiveSpawnPoints.RemoveAt(randomIndex);
        }
    */
}
