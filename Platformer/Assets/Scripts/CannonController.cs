using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform player;
    public float shootForce = 10f;
    public float cooldownTime = 2f;
    public float rotationSpeed = 5f;
    public float shootingRadius = 10f;

    private bool canShoot = true;

    private void Start()
    {
        StartCoroutine(ShootWithCooldown());
    }

    private void Update()
    {
        if (IsPlayerWithinRadius())
            RotateCannon();
    }

    private System.Collections.IEnumerator ShootWithCooldown()
    {
        while (true)
        {
            if (canShoot && IsPlayerWithinRadius())
            {
                Shoot();
                canShoot = false;
                yield return new WaitForSeconds(cooldownTime);
                canShoot = true;
            }
            yield return null;
        }
    }

    private bool IsPlayerWithinRadius()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= shootingRadius;
    }

    private void RotateCannon()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();

        Vector3 direction = (player.position - transform.position).normalized;
        ballRigidbody.AddForce(direction * shootForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }
}
