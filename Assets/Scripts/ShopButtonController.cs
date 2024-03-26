using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    public Animator shopAnimator;
    private bool isShopOpen = false;

    private void Start()
    {
        shopAnimator.SetBool("isShopOpen", isShopOpen);
    }

    public void ToggleShopState()
    {
        isShopOpen = !shopAnimator.GetBool("isShopOpen");
        shopAnimator.SetBool("isShopOpen", isShopOpen);
    }

}
