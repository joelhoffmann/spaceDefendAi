using System;
using System.Collections;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.gameObject.tag == "Enemy") 
        { 
        GetComponent<ParticleSystem>().Play();     
        if (Player.instance.shieldHealth <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;            
        }         
        
    }
    }

}