using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayerSorting : MonoBehaviour
{
    [SerializeField] GameObject _object;
    [SerializeField] Transform _playerTransform;

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _playerRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_playerTransform != null)
        {
            // Compare world positions instead of local Y values
            if (_playerTransform.position.y <= transform.position.y)
            {
                _spriteRenderer.sortingOrder = 0; // Behind player
            }
            else
            {
                _spriteRenderer.sortingOrder = 2; // In front of player
            }
        }
    }
}
