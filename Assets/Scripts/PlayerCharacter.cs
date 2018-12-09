using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
	#region SerializedFields
	[SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed;

	[SerializeField]
	private float jumpForce;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private Collider2D playerGroundCollider;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private float dashForce;

    [SerializeField]
    private AudioClip jumpClip;

    [SerializeField]
    private AudioClip deathClip;

    [SerializeField]
    private AudioClip swordSwing;

    [SerializeField]
    private float dashDrag;
    [SerializeField]
    private float walkingDrag;
	#endregion

    private AudioSource audioSource;
	private Checkpoint currentCheckpoint;
    private int DirectionalFace = 0;
    private float horizontalInput;
    private bool isOnGround;
    private bool facingRight = true;
    private bool isDead;
    private bool isDashing;


    private Animator anim;

    private Collider2D[] groundHitDetectionResults = new Collider2D[16];

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update ()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
        if (Input.GetButtonDown("Attack") && !isDead && !isDashing)
        {
            Dash();
        }
        UpdateAnimationParameters();
        TriggerRespawn();
    }
    private void FixedUpdate()
    {
        UpdatePhysicsMaterials();
        Move();
        if (!isDead)
			FlipIfNeeded();
    }

    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
    }

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void UpdatePhysicsMaterials()
    {
        if(Mathf.Abs(horizontalInput) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround && !isDead)
        {
            audioSource.PlayOneShot(jumpClip,1);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FlipIfNeeded()
    {
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
            DirectionalFace = -1;
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
            DirectionalFace = 1;
        }
    }

    private void Move()
    {
        if (!isDead && !isDashing)
        {
            rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
            Vector2 clampedVelocity = rb2d.velocity;
            clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
            rb2d.velocity = clampedVelocity;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void UpdateAnimationParameters()
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("Ground", isOnGround);
        anim.SetBool("IsDead", isDead);
    }

    public void KillPlayer()
    {
        audioSource.clip = deathClip;
        audioSource.Play();
        isDead = true;
        rb2d.freezeRotation = false;
    }

    private void TriggerRespawn()
    {
        if(isDead && Input.GetButtonDown("Respawn"))
        {
            Respawn();
        }
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
        rb2d.freezeRotation = true;
        rb2d.rotation = 0;
    }

    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        currentCheckpoint = newCurrentCheckpoint;
    }

    private void Dash()
    {
        isDashing = true;
        FreezeYAxis();
        anim.SetBool("IsDashing", isDashing);
        rb2d.drag = dashDrag;
        audioSource.clip = swordSwing;
        audioSource.Play();

        if (DirectionalFace == -1)
        {
            dashForce = 50;
            rb2d.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
        }
        else if (DirectionalFace == 1)
        {
            dashForce = -50;
            rb2d.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
        }
    }

    private void StopDashing()
    {
        UnfreezeYAxis();
        isDashing = false;
        anim.SetBool("IsDashing", isDashing);
        rb2d.drag = walkingDrag;
    }

    private void FreezeYAxis()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }

    private void UnfreezeYAxis()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
