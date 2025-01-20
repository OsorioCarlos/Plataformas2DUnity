using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailChaseState : State<Snail>
{
    [Header("Movement System")]
    [SerializeField] private float speed;
    [SerializeField] private float chaseDistance;

    private Transform target;
    private Animator animator;
    private Rigidbody2D rb;

    public override void OnEnterState(Snail controller)
    {
        base.OnEnterState(controller);
        SetRigidbody();
        SetAnimator();
        animator.SetBool("walking", false);
        animator.SetBool("running", true);
    }

    public override void OnUpdateState()
    {

    }

    public override void OnFixedUpdateState()
    {
        Movement();
    }

    public override void OnExitState()
    {
        target = null;
        animator.SetBool("running", false);
    }

    private void Movement()
    {
        if (target != null)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);
            rb.MovePosition(new Vector3(newPosition.x, rb.position.y));
            Flip();

            if (Vector3.Distance(rb.position, target.position) > chaseDistance)
            {
                controller.ChangeState(controller.PatrolState);
            }
        }
    }

    private void Flip()
    {
        if (target.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            target = collision.gameObject.transform;
        }
    }

    private void SetAnimator()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void SetRigidbody()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
}
