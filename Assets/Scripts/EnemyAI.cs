using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float visionRange = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public LayerMask obstacleMask;
    public float obstacleCheckDistance = 0.5f;

    private Transform player;
    private int currentWaypoint = 0;
    private float lastAttackTime = 0f;
    private Rigidbody2D rb;

    private Vector2 _bulletDirection = Vector2.right;
    private enum Directions { LEFT, RIGHT }
    private Directions _facingDirection = Directions.RIGHT;
    private Vector2 _moveDir = Vector2.zero;

    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= visionRange)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypoint];
        MoveTowards(targetWaypoint.position);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    void Chase()
    {
        MoveTowards(player.position);
    }

    void MoveTowards(Vector2 targetPosition)
    {

        // Handle animation and sprite flipping
        _moveDir = (targetPosition - (Vector2)transform.position).normalized;
        CalculateFacingDirection();

        if (_facingDirection == Directions.LEFT)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_facingDirection == Directions.RIGHT)
        {
            _spriteRenderer.flipX = false;
        }

        if (_moveDir.SqrMagnitude() > 0)
        {
            _animator.CrossFade("Anim_Enemy1_Run", 0);
        }
        else
        {
            _animator.CrossFade("Anim_Enemy1_Idle", 0);
        }

        // Handle obstacles
        if (!IsObstacleInPath(_moveDir))
        {
            rb.velocity = _moveDir * speed * Time.fixedDeltaTime;
        }
        else
        {
            // Simple avoidance: Try moving slightly to the left or right
            Vector2 leftCheck = Vector2.Perpendicular(_moveDir).normalized;
            Vector2 rightCheck = -leftCheck;

            if (!IsObstacleInPath(leftCheck))
            {
                rb.velocity = leftCheck * speed;
            }
            else if (!IsObstacleInPath(rightCheck))
            {
                rb.velocity = rightCheck * speed;
            }
            else
            {
                rb.velocity = Vector2.zero; // Stop if blocked completely
            }
        }
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

    bool IsObstacleInPath(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleCheckDistance, obstacleMask);
        return hit.collider != null;
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.Log("Enemy attacks!");
            lastAttackTime = Time.time;
        }
    }
}
