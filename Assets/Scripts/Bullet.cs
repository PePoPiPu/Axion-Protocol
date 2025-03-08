using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, timeAlive;
    private Vector2 _dir = Vector2.left;
    private Rigidbody2D _rb;
    private Animator _animator;
    public float currentTime = 0;
    private readonly int _animBulletExpl = Animator.StringToHash("Anim_BlueStraightBulletExplosion");

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeAlive)
        {
            currentTime = 0;
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (_rb != null)
        {
            _rb.velocity = _dir * speed;
        }
    }

    public void setDirection(Vector2 value)
    { _dir = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.CrossFade(_animBulletExpl, 0);
    }

    public void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}