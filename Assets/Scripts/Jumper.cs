using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float bounceForce;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            if (contactPoint.point.y > transform.position.y)
            {
                animator.SetTrigger("isActive");
                if (collision.gameObject.TryGetComponent<Rigidbody2D>(out var playerRb))
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
                }
            }
        }
    }
}
