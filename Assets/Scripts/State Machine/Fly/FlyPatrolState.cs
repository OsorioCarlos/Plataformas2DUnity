using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPatrolState : State<Fly>
{
    [Header("Movement System")]
    [SerializeField] private Transform route;
    [SerializeField] private float speed;
    [SerializeField] private float allyCallRange;

    private int waypointIndex = -1;
    private List<Vector3> waypoints = new List<Vector3>();
    private Vector3 currentWaypoint;
    private Animator animator;

    public override void OnEnterState(Fly controller)
    {
        base.OnEnterState(controller);
        SetAnimator();
        SetWaypoints();
        ChangeCurrentWaypoint();
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
        waypointIndex = -1;
        waypoints.Clear();
    }

    private void SetAnimator()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
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
        if (transform.position != currentWaypoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
        }
        else
        {
            ChangeCurrentWaypoint();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            /*Collider2D[] flyEnemies = Physics2D.OverlapCircleAll(transform.position, allyCallRange);
            foreach (var collider in flyEnemies)
            {
                Fly fly = collider.GetComponent<Fly>();
                if (fly != null && fly != gameObject.GetComponent<Fly>())
                {
                    fly.ChangeState(fly.AttackState);
                }
            }*/
            controller.ChangeState(controller.ChaseState);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja los rangos de detección en la vista de 
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, allyCallRange);
    }
}
