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
             	
            Player.instance.TakeDamage(damage);
            RoundManager.Instance.DecreaseEnemyCount(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);
        }

        if (collision.gameObject.tag == "Shield")
        {
                      
            Player.instance.TakeShieldDamage(damage);
            RoundManager.Instance.DecreaseEnemyCount(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);               
        }

        if (collision.gameObject.name == "Bomb")
        {
            
            RoundManager.Instance.DecreaseEnemyCount(transform.parent.gameObject);  
            Destroy(transform.parent.gameObject);                  
        }

        if (collision.gameObject.name == "EMP")
        {
           
           transform.parent.gameObject.GetComponent<EnemyAgent>().moveSpeed = 0f;  
           transform.parent.gameObject.GetComponent<EnemyAgent>().rotateSpeed = 0f;
           transform.parent.gameObject.GetComponent<EnemyAgent>().Invoke("ResetSpeed", 7f);
        }

        if (collision.gameObject.name == "Magnet")
        {
               
           transform.parent.gameObject.GetComponent<EnemyAgent>().isBeingPulled = true;
           transform.parent.gameObject.GetComponent<EnemyAgent>().magnetPosition = collision.transform.position;                       
        } 

    }
    
}
