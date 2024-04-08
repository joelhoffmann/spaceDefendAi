using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Animator shopAnimator;
    
    Transform image;    
    GameObject dragObject;
    bool draggeble = false;
    private Vector3 originalScale;
    private AudioManager m_AudioManager;

    private void Awake()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        originalScale = transform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image = transform.Find("itemImage");              
        int costNumber;
        int.TryParse(transform.Find("costText").GetComponent<TextMeshProUGUI>().text, out costNumber);

        draggeble = CoinManager.Instance.SubtractCoins(costNumber);
        if (draggeble)
        {
            dragObject = Instantiate(image.gameObject, image.position, image.rotation, transform);
            dragObject.name = transform.name;            

            dragObject.transform.parent = GameObject.Find("activeItemContainer").transform;
            shopAnimator.SetBool("isShopOpen", false); 
            ShopButtonController.instance.isShopOpen = false;           

            dragObject.transform.localScale = originalScale * 1.7f; // Hier k�nnen Sie den Vergr��erungsfaktor anpassen      

            dragObject.transform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggeble)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, dragObject.transform.position.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggeble && dragObject != null)
        {           
            // Hinzufügen eines Colliders zum dragObject
            CircleCollider2D dragCollider = dragObject.AddComponent<CircleCollider2D>();
            dragCollider.radius = 200f;
            dragCollider.isTrigger = true;

            dragObject.transform.localScale = originalScale;

            switch (dragObject.name) {
                case "Bomb":
                    m_AudioManager.PlaySFX(m_AudioManager.bomb);
                    break;
                case "EMP":
                    m_AudioManager.PlaySFX(m_AudioManager.emp);
                    break;
                case "Magnet":
                    m_AudioManager.PlaySFX(m_AudioManager.magnet);
                    break;
            }


            // wait and then destroy the dragObject
            if (dragObject.name == "Bomb" || dragObject.name == "EMP"){
            StartCoroutine(DestroyDragObject(0.1f));
            } else if (dragObject.name == "Magnet")
            {
                StartCoroutine(DestroyDragObject(5f));
            }
            
        }

    }

    private IEnumerator DestroyDragObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(dragObject);
    }

}
