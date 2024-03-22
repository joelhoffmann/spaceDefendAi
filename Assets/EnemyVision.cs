using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Collider2D viewRange;
    public SpriteRenderer spriteRenderer;

    int stuffInView = 0;
    bool playerInView = false;

    public float GetAiInput(){
        if(playerInView){
            return 1;
        }else if(stuffInView > 0){
            return -1;
        }else{
            return 0;
        }
    }

    void Update()
    {
        if(playerInView){
            spriteRenderer.color = new Color(0, 0, 1, 0.5f);
        }else if(stuffInView > 0){
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        }else{
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        stuffInView++;
        //Debug.Log("something entered view");
        if(other.gameObject.tag == "Shield"){
            playerInView = true;
            //Debug.Log("player in view");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        stuffInView--;
        //Debug.Log("something left view");
        if(other.gameObject.tag == "Shield"){
            playerInView = false;
            //Debug.Log("player left view");
        }
    }
}
