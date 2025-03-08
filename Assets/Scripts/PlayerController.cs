using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 100;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private Vector2 _moveDir = Vector2.zero;
    private enum Directions {LEFT, RIGHT}
    private Directions _facingDirection = Directions.RIGHT;

    private readonly int _animIdle = Animator.StringToHash("Anim_Player_Idle");
    private readonly int _animRun = Animator.StringToHash("Anim_Player_Run");

    private void Update()
    {
        GatherInput();
        CalculateFacingDirection();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void GatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
    }

    private void MovementUpdate()
    {
        _rb.velocity = _moveDir.normalized * _movementSpeed * Time.fixedDeltaTime;
    }

    private void CalculateFacingDirection()
    {
        if (_moveDir.x != 0)
        {
            if (_moveDir.x > 0) // Moving right
            {
                _facingDirection = Directions.RIGHT;
            }
            else if (_moveDir.x < 0) // Moving left
            {
                _facingDirection = Directions.LEFT;
            }
        }
    }

    private void UpdateAnimation()
    {
        if (_facingDirection == Directions.LEFT)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_facingDirection == Directions.RIGHT)
        {
            _spriteRenderer.flipX = false;
        }

        if (_moveDir.SqrMagnitude() > 0) // We're moving
        {
            _animator.CrossFade(_animRun, 0);
        }
        else
        {
            _animator.CrossFade(_animIdle, 0);
        }
    }
}
