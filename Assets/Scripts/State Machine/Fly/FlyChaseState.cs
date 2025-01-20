using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyChaseState : State<Fly>
{
    [Header("Movement System")]
    [SerializeField] private float speed;
    [SerializeField] private float chaseDistance;

    private Transform target;
    private Animator animator;

    public override void OnEnterState(Fly controller)
    {
        base.OnEnterState(controller);
        SetAnimator();
        animator.SetBool("running", true);
    }

    public override void OnUpdateState()
    {
        Movement();
    }

    public override void OnFixedUpdateState()
    {
        
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
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            Flip();

            if (Vector3.Distance(transform.position, target.position) > chaseDistance)
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
        if(collision.CompareTag("PlayerDetection"))
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
}
