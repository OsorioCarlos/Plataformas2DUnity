using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPatrolState : State<Snail>
{
    [Header("Movement System")]
    [SerializeField] private Transform route;
    [SerializeField] private float speed;
    [SerializeField] private float waitingTime;

    private float waitingTimer = 0;
    private int waypointIndex = -1;
    private List<Vector3> waypoints = new List<Vector3>();
    private Vector3 currentWaypoint;
    private Animator animator;
    private Rigidbody2D rb;

    public override void OnEnterState(Snail controller)
    {
        base.OnEnterState(controller);
        SetRigidbody();
        SetAnimator();
        SetWaypoints();
        ChangeCurrentWaypoint();
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
        waitingTimer = 0;
        waypointIndex = -1;
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

    private void SetWaypoints()
    {
        if (waypoints.Count == 0)
        {
            foreach (Transform t in route)
            {
                waypoints.Add(t.position);
            }
        }
    }

    private void Movement()
    {
        if (Vector3.Distance(rb.position, currentWaypoint) > 0.1f)
        {
            rb.MovePosition(Vector3.MoveTowards(rb.position, currentWaypoint, speed * Time.fixedDeltaTime));
        }
        else
        {
            StartDelayTimer();
        }
    }

    private void ChangeCurrentWaypoint()
    {
        waypointIndex++;
        if (waypointIndex > waypoints.Count - 1)
        {
            waypointIndex = 0;
        }
        currentWaypoint = waypoints[waypointIndex];
        Flip();
        StopDelayTimer();
    }

    private void Flip()
    {
        if (currentWaypoint.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void StartDelayTimer()
    {
        if (animator.GetBool("walking"))
        {
            animator.SetBool("walking", false);
        }
        else
        {
            waitingTimer += 1 * Time.deltaTime;
            if (waitingTimer > waitingTime)
            {
                ChangeCurrentWaypoint();
            }
        }
    }

    private void StopDelayTimer()
    {
        animator.SetBool("walking", true);
        waitingTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            controller.ChangeState(controller.ChaseState);
        }
    }
}