using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float moveSpeed = 3f;

    private Transform targetPoint;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        targetPoint = point1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            SwitchTarget();
        }
    }

    void SwitchTarget()
    {
        targetPoint = (targetPoint == point1) ? point2 : point1;
        FlipSprite();
    }

    void FlipSprite()
    {
        if (targetPoint.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
