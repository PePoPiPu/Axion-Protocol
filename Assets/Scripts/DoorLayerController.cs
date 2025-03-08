using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLayerController : MonoBehaviour
{
    [SerializeField] GameObject _door;
    [SerializeField] GameObject _player;
    [SerializeField] Transform _playerTransform;

    private SpriteRenderer _doorRenderer;
    private SpriteRenderer _playerRenderer;

    private void Start()
    {
        _doorRenderer = GetComponent<SpriteRenderer>();
        _playerRenderer = _player.GetComponent<SpriteRenderer>();
    }

    public void DisableCollider()
    {
        _door.GetComponent<Collider2D>().enabled = false;
    }

    public void EnableCollider()
    {
        _door.GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        if (_playerTransform != null)
        {
            // Compare world positions instead of local Y values
            if (_playerTransform.position.y <= transform.position.y)
            {
                _doorRenderer.sortingOrder = 0; // Behind player
            }
            else
            {
                _doorRenderer.sortingOrder = 2; // In front of player
            }
        }
    }
}
