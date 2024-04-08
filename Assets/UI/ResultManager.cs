using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private VisualElement rootElement;


    private void OnEnable()
    {
        rootElement = uiDoc.rootVisualElement;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
