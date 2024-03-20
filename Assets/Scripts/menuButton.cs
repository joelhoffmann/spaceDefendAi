using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class menuButton : MonoBehaviour
{
    public GameObject popupWindow;

    private bool isPopupOpen = false;

    public void TogglePopup()
    {
        if (isPopupOpen)
        {
            // Wenn das Pop-up-Fenster bereits geöffnet ist, dann schließe es.
            popupWindow.SetActive(false);
            print("test");
            isPopupOpen = false;
        }
        else
        {
            // Wenn das Pop-up-Fenster geschlossen ist, dann öffne es.
            popupWindow.SetActive(true);
            isPopupOpen = true;
        }
    }
}
