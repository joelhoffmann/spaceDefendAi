using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variablen deklarieren    
    public int health;

    public int shieldHealth;
    public int experience;
  //  public int coins;

   // public int wallCost; // Cost of the wall in coins --> Besser im GameManager

    public static Player instance;

  // public GameObject wallPrefab; // Prefab für ein Wandsegment
   // public float segmentLength = 0.1f; // Länge jedes Wandsegments
 //   private GameObject currentSegment; // Das aktuelle instanzierte Wandsegment
  //  private Vector2 lastMousePosition; // Die letzte aufgezeichnete Mausposition
  //  private List<GameObject> spawnedWalls = new List<GameObject>();

            
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        instance = this;               
    }
    
    // Start is called before the first frame update
    void Start()
    {
                         
    }           
    
    void Update()
    {
      //  if (Input.GetMouseButton(0) && coins >= wallCost) // Check if left mouse button is held down
      //  {
    //        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
     //       if (Vector2.Distance(mousePos, lastMousePosition) > segmentLength / 2) // Check if distance moved is greater than half the segment length
    //        {
      //        //  CreateWallSegment(mousePos);
      //         lastMousePosition = mousePos;
      //      }
        //}
    }

    /*

    void CreateWallSegment(Vector2 position)
    {        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Shield" || collider.gameObject.tag == "Enemy")
            {
                return;
            }
        }        
        
        coins -= wallCost; // Deduct the cost of the wall from the player's coins
        Debug.Log("Coins: " + coins);
        if (currentSegment == null) // If no segment exists, create one
        {
            currentSegment = Instantiate(wallPrefab, position, Quaternion.identity, transform);     
            spawnedWalls.Add(currentSegment);       
            return;
        }

        float distance = Vector2.Distance(currentSegment.transform.position, position);
        if (distance >= segmentLength) // Check if the distance is sufficient to create a new segment
        {
            currentSegment = Instantiate(wallPrefab, position, Quaternion.identity, transform);
            spawnedWalls.Add(currentSegment);
        }
    }    
    */

 // Loose 10 health points when the player is hit by an enemy
    public void TakeDamage(int damage)
    {       
    //    Debug.Log("Player Hit!");

        health -= damage;
        //Debug.Log("Player health: " + health);
        if (health <= 0)
        {
            Die();
        }        
    }

    void Die()
    {
        //Debug.Log("Player died");      
        Destroy(gameObject);                
    }

    public void ReceiveCoins(int amount)
    {
        CoinManager.Instance.AddCoins(amount);
       // Debug.Log("Received " + amount + " coins. Total coins: " + CoinManager.Instance.GetCoins());
    }   

   /* // Methode zum Löschen der Wände
    public void ClearWalls()
    {
        // Iteriere über die Liste der geklonten Wände und zerstöre jedes einzelne
        foreach (GameObject wall in spawnedWalls)
        {
            Destroy(wall);
        }

        // Leere die Liste, nachdem alle Wände zerstört wurden
        spawnedWalls.Clear();
    }
    */
    

    public void TakeShieldDamage(int damage)
    {       
        shieldHealth -= damage;
      //  Debug.Log("Shield health: " + shieldHealth);

        if (shieldHealth < 0)
        {
            DestroyShield();
           //Debug.Log("Shield destroyed");
        }

    }

    private void DestroyShield()
    {             
        // Deaktiviere das Schild-GameObject
        GameObject shield = GameObject.FindGameObjectWithTag("Shield");
        shield.SetActive(false);          
       
    }
}