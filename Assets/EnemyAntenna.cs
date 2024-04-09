using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

public class EnemyAntenna : MonoBehaviour
{   
    public int detectedCollisionType = 0;
    public int actualCollisionType = 0;

    public int GetCollision(){
        actualCollisionType = detectedCollisionType;
        ResetCollision();        
        return actualCollisionType;             
    }

    private void ResetCollision()
    {
        detectedCollisionType = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {        
         if (collision.gameObject.tag == "Base")
        {
            Debug.Log("Base Hit");	
            detectedCollisionType = 1;
        }

        if (collision.gameObject.tag == "Shield")
        {
            Debug.Log("Shield Hit");
            detectedCollisionType = 2;               
        }

        if (collision.gameObject.name == "Bomb")
        {
            Debug.Log("Bomb Hit");
            detectedCollisionType = 3;               
        }

        if (collision.gameObject.name == "EMP")
        {
            Debug.Log("EMP Hit");
            detectedCollisionType = 4;               
        }

        if (collision.gameObject.name == "Magnet")
        {
            Debug.Log("Magnet Hit");
            detectedCollisionType = 5;               
        } 

    }
    
}
