using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private void Start()
    {
        _animator.SetBool("isIn", false);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            _animator.SetBool("isIn", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _animator.SetBool("isIn", false);
        }
    }
}
