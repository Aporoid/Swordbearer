using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private float horizontalInput;
    private bool isOnGround;
    bool facingRight = true;

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
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();
    }

    private void Move()
    {
        rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rb2d.velocity;
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;
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
    }
}
