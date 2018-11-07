using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 5f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    private Checkpoint currentCheckpoint;
    private float horizontalInput;
    private bool isOnGround;
    bool facingRight = true;
    private bool isDead;

    Animator anim;

    private Collider2D[] groundHitDetectionResults = new Collider2D[16];

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
        SwordAttack();
        UpdateAnimationParameters();
    }

    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
    }

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround && !isDead)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Move();
        if (!isDead) FlipIfNeeded();
    }

    private void FlipIfNeeded()
    {
        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();
    }

    private void Move()
    {
        if (!isDead)
        {
            rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
            Vector2 clampedVelocity = rb2d.velocity;
            clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
            rb2d.velocity = clampedVelocity;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void UpdateAnimationParameters()
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("Ground", isOnGround);
        anim.SetBool("IsDead", isDead);
    }

    public void KillPlayer()
    {
        isDead = true;
        Respawn();
    }

    public void Respawn()
    {
        isDead = false;
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            transform.position = currentCheckpoint.transform.position;

        }    
    }

    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        currentCheckpoint = newCurrentCheckpoint;
    }

    void SwordAttack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            anim.SetBool("IfAttack", true);
            
        }
    }
}
