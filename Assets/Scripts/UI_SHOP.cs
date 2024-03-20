using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SHOP : MonoBehaviour
{

    private Transform container;
    private Transform shopItemTemplate;
    public Sprite bombSprite; 
    public Sprite magnetSprite; 
    public Sprite empSprite; 

    private void Awake()
    {
        container = transform.Find("grid");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton("Bomb", 200, bombSprite);
        CreateItemButton("Magnet", 300, magnetSprite);
        CreateItemButton("EMP", 400, empSprite);
    }

    private void CreateItemButton(string itemName, int itemCost, Sprite itemSprite)
    {

        Transform shopItemTransform = Instantiate(shopItemTemplate, container);

        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().text = itemCost.ToString();
        Image itemImage = shopItemTransform.Find("itemImage").GetComponent<Image>();
        if (itemSprite != null)
        {
            itemImage.sprite = itemSprite;
        }
        else
        {
            Debug.LogWarning("Sprite für " + itemName + " ist nicht gesetzt.");
        }
        shopItemTransform.gameObject.SetActive(true);
        shopItemTransform.gameObject.name = itemName;
    }

}
