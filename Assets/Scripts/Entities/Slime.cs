using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Movement System")]
    [SerializeField] private float speed;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask whatIsSolid;

    private int direction = 1;
    private Rigidbody2D rb;
    private HealthSystem healthSystem;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        if (CrashIntoWall())
        {
            Flip();
        }
    }

    private void Flip()
    {
        direction *= -1;
        if (direction == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCollision(collision);
    }

    private void PlayerCollision(Collider2D collision)
    {
        if (collision.CompareTag("Player") && healthSystem.IsDead() == false)
        {
            HealthSystem playerHealthSystem = collision.GetComponent<HealthSystem>();
            Player playerScript = collision.GetComponent<Player>();

            if (playerScript.RaycastPoint.y > transform.position.y)
            {
                ReceiveDamage(playerHealthSystem.Damage);
                if (collision.TryGetComponent<Rigidbody2D>(out var playerRb))
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, playerScript.JumpForceInEnemy);
                }
            }
            else
            {
                playerHealthSystem.TakeDamage(healthSystem.Damage);
            }
        }
    }

    private void ReceiveDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        if (healthSystem.IsDead() == true)
        {
            direction = 0;
            anim.SetTrigger("isDead");
        }
    }

    private bool CrashIntoWall()
    {
        return Physics2D.Raycast(raycastPoint.position, new Vector2(direction, 0), raycastDistance, whatIsSolid);
    }
}
