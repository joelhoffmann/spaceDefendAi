using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    public float spawnInterval = 2f; // Intervall f�r das Spawnen von Feinden
    public int startEnemyCount = 3; // Anzahl der Feinde zu Beginn
    public int enemyIncreasePerRound = 1; // Anzahl der zus�tzlichen Feinde pro Runde

    private Transform container;
    private Transform enemyTemplate;
    private GameObject enemyPrefab; // Das Feind-Prefab
    private int currentRound = 1; // Aktuelle Runde
    private List<GameObject> currentEnemies = new List<GameObject>(); // Liste der aktuellen Feinde
    private bool lockRound = false;

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
        // Spawnen Sie die Feinde f�r die aktuelle Runde
        for (int i = 0; i < startEnemyCount + (enemyIncreasePerRound * (currentRound - 1)); i++)
        {
            SpawnEnemy();
        }
    }    


    void SpawnEnemy()
    {
        // W�hlen Sie eine zuf�llige Position um den Bildschirmrand aus
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();

        GameObject newEnemyObject = Instantiate(enemyPrefab, container);
        newEnemyObject.transform.position = randomSpawnPosition;
        newEnemyObject.SetActive(true);

        currentEnemies.Add(newEnemyObject);
    }

    Vector3 GetRandomSpawnPosition()
    {
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
    }

    public void DecreaseEnemyCount(GameObject enemy)
    {
        Debug.Log("----------------sdffghgd-------------------");
        currentEnemies.Remove(enemy);
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
}
