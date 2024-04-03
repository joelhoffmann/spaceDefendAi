using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    public Animator shopAnimator;
    public bool isShopOpen = false;  
    public static ShopButtonController instance;     
  
    private void Start()
    {
        instance = this; 
        shopAnimator.SetBool("isShopOpen", isShopOpen);                           
    }

    public void ToggleShopState()
    {
        isShopOpen = !shopAnimator.GetBool("isShopOpen");
        shopAnimator.SetBool("isShopOpen", isShopOpen);        
    }
    
    // get the current state of the shop
    public bool GetShopState()
    {
        return isShopOpen;
    }
}
