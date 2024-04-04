using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Collider2D viewRange;
    public SpriteRenderer spriteRenderer;

    int stuffInView = 0;

    public float GetAiInput(){
        return stuffInView;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered" + other.gameObject.tag);
        if(other.gameObject.tag == "Shield" || other.gameObject.tag == "Base"){
            return;
        }
        stuffInView++;
        spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Shield" || other.gameObject.tag == "Base"){
            return;
        }
        stuffInView--;
        if(stuffInView == 0){
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
    }
}
