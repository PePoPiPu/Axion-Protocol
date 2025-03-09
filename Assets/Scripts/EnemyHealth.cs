using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Sprite _health6;
    [SerializeField] Sprite _health5;
    [SerializeField] Sprite _health4;
    [SerializeField] Sprite _health3;
    [SerializeField] Sprite _health2;
    [SerializeField] Sprite _health1;
    [SerializeField] Image _healthBar;
    [SerializeField] Image _shieldBar;

    private Animator _animator;
    private Rigidbody2D _rb;
    public int _health = 6;
    private int _shield = 6;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            _health -= 1;
            ChangeHealthBar(_health);
        }
    }

    private void ChangeHealthBar(int health)
    {
        if(_shieldBar == null || _shield == 0)
        {
            switch (health)
            {
                case 6:
                    _healthBar.sprite = _health6;
                    break;
                case 5:
                    _healthBar.sprite = _health5;
                    break;
                case 4:
                    _healthBar.sprite = _health4;
                    break;
                case 3:
                    _healthBar.sprite = _health3;
                    break;
                case 2:
                    _healthBar.sprite = _health2;
                    break;
                case 1:
                    _healthBar.sprite = _health1;
                    break;
                case 0:
                    Die();
                    break;
                default:
                    break;
            }
        }
    }

    private void Die()
    {
        _animator.CrossFade("Anim_Enemy1_Death", 0);
        // Lock enemy position upon death
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
}
