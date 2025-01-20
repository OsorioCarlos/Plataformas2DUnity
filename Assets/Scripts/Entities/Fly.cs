using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private HealthSystem healthSystem;

    //State Machine
    private FlyPatrolState patrolState;
    private FlyChaseState chaseState;
    private FlyAttackState attackState;
    private FlyDeathState deathState;
    private State<Fly> currentState;

    public FlyPatrolState PatrolState { get => patrolState; }
    public FlyChaseState ChaseState { get => chaseState; }
    public FlyAttackState AttackState { get => attackState; }

    // Start is called before the first frame update
    void Start()
    {
        patrolState = GetComponent<FlyPatrolState>();
        chaseState = GetComponent<FlyChaseState>();
        attackState = GetComponent<FlyAttackState>();
        deathState = GetComponent<FlyDeathState>();
        healthSystem = GetComponent<HealthSystem>();

        ChangeState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdateState();
        }
    }

    void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnFixedUpdateState();
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
            ChangeState(deathState);
        }
    }

    public void ChangeState(State<Fly> newState)
    {
        if (currentState != null)
        {
            currentState.OnExitState();
        }
        currentState = newState;
        currentState.OnEnterState(this);
    }
}
