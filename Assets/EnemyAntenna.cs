using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

public class EnemyAntenna : MonoBehaviour
{   
    public int damage = 10;      

    void OnTriggerEnter2D(Collider2D collision)
    {        
         if (collision.gameObject.tag == "Base")
        {
            Debug.Log("Base Hit");    	
            Player.instance.TakeDamage(damage);
            RoundManager.Instance.DecreaseEnemyCount(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);
        }

        if (collision.gameObject.tag == "Shield")
        {
            Debug.Log("Shield Hit");           
            Player.instance.TakeDamage(damage);
            RoundManager.Instance.DecreaseEnemyCount(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);               
        }

        if (collision.gameObject.name == "Bomb")
        {
            Debug.Log("Bomb Hit");      
            Player.instance.TakeDamage(damage);
            RoundManager.Instance.DecreaseEnemyCount(transform.parent.gameObject);                   
        }

        if (collision.gameObject.name == "EMP")
        {
            Debug.Log("EMP Hit");
           transform.parent.gameObject.GetComponent<EnemyAgent>().moveSpeed = 0f;  
           transform.parent.gameObject.GetComponent<EnemyAgent>().rotateSpeed = 0f;
           transform.parent.gameObject.GetComponent<EnemyAgent>().Invoke("ResetSpeed", 5f);
        }

        if (collision.gameObject.name == "Magnet")
        {
            Debug.Log("Magnet Hit");    
           transform.parent.gameObject.GetComponent<EnemyAgent>().isBeingPulled = true;
           transform.parent.gameObject.GetComponent<EnemyAgent>().magnetPosition = collision.transform.position;                       
        } 

    }
    
}
