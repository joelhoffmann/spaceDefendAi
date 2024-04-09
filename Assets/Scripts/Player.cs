using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    public int health;

    public int shieldHealth;
    public int experience;
    public static Player instance;

    public event Action OnPlayerDeath;

    private float timer = 0f;
    public float interval = 1f; 
    
    void Awake()
    {
        instance = this;
    }
  
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
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void ReceiveCoins(int amount)
    {                                 
        CoinManager.Instance.AddCoins(amount);        
    }

    public void ReceiveExp(int amount)
    {                                 
        ExpManager.Instance.AddExp(amount);        
    }


    public void TakeShieldDamage(int damage)
    {
        shieldHealth -= damage;

        GameObject shield = GameObject.FindGameObjectWithTag("Shield");
        Color color = shield.GetComponent<SpriteRenderer>().color;
        color.a -= 0.045f;
        shield.GetComponent<SpriteRenderer>().color = color;

        if (shieldHealth == 0)
        {
            DestroyShield();
        }
    } 

    private void DestroyShield()
    {
        GameObject shield = GameObject.FindGameObjectWithTag("Shield");
        shield.SetActive(false);
    }

    IEnumerator CallMethodAfterDelay()
    {
        ReceiveExp(5);
        yield return new WaitForSeconds(interval);
    }

    public void Respawn()
    {
        health = 100;
        shieldHealth = 100;
        experience = 0;
        gameObject.SetActive(true);
    }
}