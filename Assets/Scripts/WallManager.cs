using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Wall _wallPreFab;
    private Wall _currentWall; 

    public const float RESOLUTION = 0.1f;

    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);        
       
        if (Input.GetMouseButtonDown(0) && CoinManager.Instance.GetCoins() >= CoinManager.Instance.wallCost && ShopButtonController.instance.GetShopState() == false)
        {
            _currentWall = Instantiate(_wallPreFab, mousePos, Quaternion.identity);
        }
        if (CoinManager.Instance.GetCoins() < CoinManager.Instance.wallCost)
        {
            _currentWall.enabled = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _currentWall.enabled = false;
        }

        //if(Input.GetMouseButton(0)) _currentWall.SetPosition(mousePos);
    }     
 
}
