using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    private readonly List<Vector2> _linePositions = new List<Vector2>();
    public bool enabled = true;
    void Start()
    {
        _renderer.positionCount = 0; // Setze die Anfangszahl der Linienpunkte auf 0
        _collider.transform.position -= transform.position;        
        gameObject.tag = "Wall";       
        _collider.tag = "Wall";
    }

    void Update()
    {
        if (enabled)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; 
            _linePositions.Add(mousePos);

            UpdateLineRenderer();
        }
    }

    private void UpdateLineRenderer()
    {   
        // Cancel wall if it collides with shield, enemy or base
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_linePositions[_linePositions.Count - 1], 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Shield" || collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Base")      
               
            {             
                enabled = false;
                return;
            }
        }         

        CoinManager.Instance.SubtractCoins(CoinManager.Instance.wallCost);
        _renderer.positionCount = _linePositions.Count;       

        for (int i = 0; i < _linePositions.Count; i++)
        {
            _renderer.SetPosition(i, _linePositions[i]);
            _collider.points = _linePositions.ToArray();  
        }
    }

    //deprecated
    public void SetPosition(Vector2 position)
    {
        if (!CanAppend(position)) return;

        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, position);
    }

    private bool CanAppend(Vector2 position)
    {
        if(_renderer.positionCount == 0) return false;
        
        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), position) > WallManager.RESOLUTION;
    }
}