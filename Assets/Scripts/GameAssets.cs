using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetsStorrage : MonoBehaviour
{
    private static GameAssetsStorrage _i;

    public static GameAssetsStorrage i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssetsStorrage")) as GameObject).GetComponent<GameAssetsStorrage>();
            return _i;
        }
    }

    public Sprite sprite;
}
