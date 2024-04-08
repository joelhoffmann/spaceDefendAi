using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject nexatronPrefab;
    public GameObject baseTarget;
    public Transform[] spawnPoints;
    public float initialWaveTime = 5f; // Time for the initial wave
    public float waveTimeIncrement = 5f; // Incremental time added for each wave
    public float spawnDelay = 1f; // Delay between spawning each enemy

    private int waveSize;
    private int waveNumber = 1;
    private float nextWaveTime;

    void Start()
    {
        nextWaveTime = Time.time + initialWaveTime;
        UpdateWaveSize();
    }

    void Update()
    {
        if (Time.time > nextWaveTime)
        {
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        SpawnWave();
        waveNumber++;
        nextWaveTime = Time.time + initialWaveTime + waveTimeIncrement * waveNumber;
        UpdateWaveSize();
    }

    void UpdateWaveSize()
    {
        waveSize = waveNumber * 2 + 1; // Example formula for wave size
    }

    void SpawnWave()
    {
        for (int i = 0; i < waveSize; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points defined for enemy spawning.");
            return;
        }
        if (enemyPrefab == null)
        {
            Debug.LogError("No enemy prefab defined for enemy spawning.");
            return;
        }
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(nexatronPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
