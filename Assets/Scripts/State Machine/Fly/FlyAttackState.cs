using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyAttackState : State<Fly>
{
    [Header("Movement System")]
    [SerializeField] private float speed;
    [SerializeField] private float proximityDistance;

    private Transform target;

    public override void OnEnterState(Fly controller)
    {
        base.OnEnterState(controller);
        SetTarget();
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
    }

    private void Movement()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            Flip();
            if (Vector3.Distance(transform.position, target.position) < proximityDistance)
            {
                controller.ChangeState(controller.ChaseState);
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
    
    private void SetTarget()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
    }
}
