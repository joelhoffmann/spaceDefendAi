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
    public static Player instance;

    public event Action OnPlayerDeath;

    private float timer = 0f;
    public float interval = 1f; // Change this value to adjust the interval
            
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
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            StartCoroutine(CallMethodAfterDelay());
        }          
    }

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
        Debug.Log("Player died");  
           // Informieren Sie den RoundManager, dass dieser Feind gestorben ist
       // RoundManager.Instance.DecreaseEnemyCount(gameObject);

        OnPlayerDeath?.Invoke();


        // Kill the instance of the player
        gameObject.SetActive(false);                   
    }

    public void ReceiveCoins(int amount)
    {   
    //    Debug.Log("Received " + amount + " coins. Total coins: " + CoinManager.Instance.GetCoins());                          
        CoinManager.Instance.AddCoins(amount);
       // Debug.Log("Received " + amount + " coins. Total coins: " + CoinManager.Instance.GetCoins());
    }   

     public void ReceiveExp(int amount)
    {   
    //    Debug.Log("Received " + amount + " coins. Total coins: " + CoinManager.Instance.GetCoins());                          
        ExpManager.Instance.AddExp(amount);
       // Debug.Log("Received " + amount + " coins. Total coins: " + CoinManager.Instance.GetCoins());
    }    
    

    public void TakeShieldDamage(int damage)
    {       
        shieldHealth -= damage;
      //  Debug.Log("Shield health: " + shieldHealth);

        if (shieldHealth == 0)
        {
            DestroyShield();
           //Debug.Log("Shield destroyed");
        }

    }

    private void DestroyShield()
    {
        // Deaktiviere das Schild-GameObject
        //OnPlayerDeath?.Invoke();
        GameObject shield = GameObject.FindGameObjectWithTag("Shield");
        shield.SetActive(false);
    }

     IEnumerator CallMethodAfterDelay()
    {
        // Call your method here
        ReceiveExp(5);

        // Wait for the specified delay before executing again
        yield return new WaitForSeconds(interval);
    }

    // Respawn player if he dies also create the game object again
    
    public void Respawn()
    {
        health = 100;
        shieldHealth = 100;
        experience = 0;
        gameObject.SetActive(true);   
    }
}