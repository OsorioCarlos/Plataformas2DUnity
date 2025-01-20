using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement System")]
    [SerializeField] private float speed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask whatIsJumping;

    private bool isPlayerDead;
    private bool levelCompleted;
    private bool usingLadder;
    private float gravityScale;
    private float jumpForceInEnemy;
    private Rigidbody2D rb;
    private Animator animator;
    private HealthSystem healthSystem;
    private UISystem UISystem;

    private List<string> inventory = new List<string>();

    public Vector3 RaycastPoint { get => raycastPoint.position; }
    public float JumpForceInEnemy { get => jumpForceInEnemy; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
        UISystem = GetComponent<UISystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlayerDead = false;
        levelCompleted = false;
        usingLadder = false;
        jumpForceInEnemy = jumpForce * 0.70f;
        gravityScale = rb.gravityScale;
        healthSystem.EnableUIUpdate(UISystem);
        UISystem.InitializeInventoryUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelCompleted == false)
        {
            if (healthSystem.IsDead() == false)
            {
                if (usingLadder == true)
                {
                    LadderMovement();
                }
                else
                {
                    Movement();
                    Jump();
                }
            }
            else
            {
                PlayerDead();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            usingLadder = true;
            rb.gravityScale = 0;
        }

        if (collision.CompareTag("Finish"))
        {
            levelCompleted = true;
            animator.SetBool("running", false);
            rb.velocity = Vector3.zero;
            UISystem.ShowLevelCompleteUI();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            usingLadder = false;
            animator.SetBool("running", false);
            animator.SetBool("usingLadder", false);
            rb.gravityScale = gravityScale;
        }
    }

    private void Movement()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * speed, rb.velocity.y);

        if (inputH != 0)
        {
            animator.SetBool("running", true);
            if (inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            } else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        } else
        {
            animator.SetBool("running", false);
        }
    }

    private void LadderMovement()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(inputH * speed, inputV * climbSpeed);

        if (animator.GetBool("usingLadder") == false)
        {
            if (inputH != 0)
            {
                animator.SetBool("running", true);
                if (inputH > 0)
                {
                    transform.eulerAngles = Vector3.zero;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
            }
            else
            {
                animator.SetBool("running", false);
            }

            if (inputV != 0)
            {
                animator.SetBool("usingLadder", true);
            }

        }
        else
        {
            if (inputV != 0)
            {
                animator.SetBool("running", true);
            } else
            {
                animator.SetBool("running", false);
            }
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jumping");
        }
    }

    private bool OnGround()
    {
        return Physics2D.Raycast(raycastPoint.position, Vector2.down, raycastDistance, whatIsJumping);
    }

    private void PlayerDead()
    {
        if (isPlayerDead == false)
        {
            isPlayerDead = true;
            animator.SetTrigger("isDead");
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(overlapCirclePoint.position, attackRadius);
    }

    public void GameOver()
    {
        UISystem.ShowGameOverUI();
        gameObject.SetActive(false);
    }

    public void SaveItemInInventory(string item)
    {
        inventory.Add(item);
        UISystem.UpdateInventoryUI(item);
    }

    public bool ExistsItemInInventory(string item)
    {
        return inventory.Contains(item);
    }
}
