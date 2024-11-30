using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    // [SerializeField] float stopDistance = 1.4f; DO NOT DELETE!

    Transform targetTransform;

    Rigidbody2D rb;
    KnockBack knockBack;
    Health health;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        health = GetComponent<Health>();
    }

    void Start()
    {
        targetTransform = PlayerController.Instance.transform;
        rb.AddForce(Vector2.left * 20);
    }

    void Update()
    {
        if (targetTransform != null)
        {
            FlipEnemySprite();
        }
    }

    void FixedUpdate()
    {
        if (targetTransform != null && !knockBack.isKnockedBack && !health.isEnemyDead)
        {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        Vector2 position = rb.position;
        Vector2 targetPosition = targetTransform.position;
        Vector2 newPosition = Vector2.MoveTowards(position, targetPosition, movementSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    // Currently not in use
    // DO NOT DELETE!
    // void FollowTargetWithStoppingPoint()
    // {
    //     Vector2 direction = targetTransform.position - transform.position;
    //     float distance = direction.magnitude;

    //     if (distance > stopDistance)
    //     {
    //         Vector2 directionNormalized = direction.normalized;
    //         Vector2 targetPosition = (Vector2)targetTransform.position - directionNormalized * stopDistance;
    //         transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    //     }
    // }

    void FlipEnemySprite()
    {
        if (transform.position.x > targetTransform.position.x)
        {
            transform.eulerAngles = new Vector3(0, -180f, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // TODO Prevent enemies to overlap each others if necessary 
}
