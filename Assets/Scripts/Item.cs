using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Bomb,
        EMP,
        Magnet
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Bomb: return 200;
            case ItemType.EMP: return 400;
            case ItemType.Magnet: return 300;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        return null;
    }
}
