using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    public Animator shopAnimator;
    public bool isShopOpen = false;  
    public static ShopButtonController instance;
    private AudioManager m_AudioManager;

    private void Awake()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        instance = this; 
        shopAnimator.SetBool("isShopOpen", isShopOpen);                           
    }

    public void ToggleShopState()
    {
        isShopOpen = !shopAnimator.GetBool("isShopOpen");
        shopAnimator.SetBool("isShopOpen", isShopOpen);
        m_AudioManager.PlaySFX(m_AudioManager.shopOpen);
    }
    
    // get the current state of the shop
    public bool GetShopState()
    {
        return isShopOpen;
    }
}
