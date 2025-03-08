using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 100;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Light2D _flashlight;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObjectPool bulletPool;
    [SerializeField] GameObject _player;
    [SerializeField] ScreenShake _screenShake;

    private Vector2 _moveDir = Vector2.zero;
    private enum Directions {LEFT, RIGHT}
    private Directions _facingDirection = Directions.RIGHT;
    private bool _isFlashlightOn = false;
    private Vector2 _bulletDirection = Vector2.right;
    private Vector2 _playerCoords;

    private readonly int _animIdle = Animator.StringToHash("Anim_Player_Idle");
    private readonly int _animRun = Animator.StringToHash("Anim_Player_Run");
    private readonly int _animShoot = Animator.StringToHash("Anim_Player_Shoot");

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

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(_isFlashlightOn)
            {
                _flashlight.enabled = false;
                _isFlashlightOn = false;
            } 
            else
            {
                _flashlight.enabled = true;
                _isFlashlightOn = true;
            }
        }
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
                _bulletDirection = Vector2.right;
            }
            else if (_moveDir.x < 0) // Moving left
            {
                _facingDirection = Directions.LEFT;
                _bulletDirection = Vector2.left;
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
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (Input.GetMouseButtonDown(0) && !stateInfo.IsName("Anim_Player_Shoot"))
            {
                _animator.CrossFade(_animShoot, 0);

                // Shake screen
                StartCoroutine(_screenShake.Shaking());

                // Shoot bullet
                if(_facingDirection == Directions.LEFT)
                {
                     _playerCoords = ((Vector2)_player.transform.position) - new Vector2(0.4f, 0.3f);
                }
                else
                {
                    _playerCoords = ((Vector2)_player.transform.position) - new Vector2(-0.4f, 0.3f);
                }

                GameObject o = bulletPool.GetAvailableGameObject();
                o.SetActive(true);
                o.transform.position = _playerCoords;
                o.GetComponent<Bullet>().setDirection(_bulletDirection);
            }
            else if (!stateInfo.IsName("Anim_Player_Shoot") || stateInfo.normalizedTime >= 1.0f)
            {
                _animator.CrossFade(_animIdle, 0);
            }
        }
    }
}
