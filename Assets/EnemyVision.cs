using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Collider2D viewRange;
    public SpriteRenderer spriteRenderer;

    int stuffInView = 0;
    bool playerInView = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    void OnTriggerStay2D(Collider2D other)
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        stuffInView++;
        Debug.Log("something entered view");
        if(other.gameObject.tag == "Shield"){
            playerInView = true;
            Debug.Log("player in view");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        stuffInView--;
        Debug.Log("something left view");
        if(other.gameObject.tag == "Shield"){
            playerInView = false;
            Debug.Log("player left view");
        }
    }
}
