using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private Transform container;
    private Transform enemyTemplate;

    private static EnemyManager instance;

    // Initialisierungsmethode
    public void Init()
    {
    }
    private void Awake()
    {
        //container = GameObject.Find("activeEnemyContainer").transform;
        // enemyTemplate = container.Find("enemyTemplate");
        // enemyTemplate.gameObject.SetActive(false);

    }

    // Singleton-Instanz
    public static EnemyManager Instance
    {
        get
        {
            GameObject enemyObject = GameObject.Find("EnemyManager");
            if (instance == null)
            {
                instance = enemyObject.AddComponent<EnemyManager>();
            }
            return instance;
        }
    }

    private void Start()
    {
        //InvokeRepeating("CreateEnemy", 0f, 2f);
    }

    private void CreateEnemy(string itemName)
    {
        //Debug.Log("Spawn");
        //Transform enemyTransform = Instantiate(enemyTemplate, container);
        //enemyTransform.gameObject.SetActive(true);
        //enemyTransform.gameObject.name = itemName;
    }    
   
}