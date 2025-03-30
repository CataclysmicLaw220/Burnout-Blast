using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float crouchSpeed = 2.5f;
    public float slamGravityMultiplier = 3f; // How much gravity increases during ground slam
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isCrouching;
    private bool isSlamming;
    private float originalHeight;
    private float originalGravityScale;
    private CapsuleCollider2D playerCollider;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        originalHeight = playerCollider.size.y;
        originalGravityScale = rb.gravityScale; // Store the default gravity
    }

    void Update() {
        // Check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Get movement input
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Check for crouch input
        isCrouching = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl);

        // Apply movement
        float currentSpeed = isCrouching && isGrounded ? crouchSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);

        // Jumping
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Ground Slam (Increased Gravity)
        if (!isGrounded && isCrouching && rb.linearVelocity.y < 0) {
            StartGroundSlam();
        }

        // Stop slam when hitting the ground
        if (isSlamming && isGrounded) {
            StopGroundSlam();
        }

        // Adjust collider when crouching
        if (isCrouching && isGrounded) {
            playerCollider.size = new Vector2(playerCollider.size.x, originalHeight * 0.5f);
        } else {
            playerCollider.size = new Vector2(playerCollider.size.x, originalHeight);
        }
    }

    void StartGroundSlam() {
        if (!isSlamming) {
            isSlamming = true;
            rb.gravityScale *= slamGravityMultiplier; // Increase gravity for faster fall
            Debug.Log("Ground Slam Started!");
        }
    }

    void StopGroundSlam() {
        isSlamming = false;
        rb.gravityScale = originalGravityScale; // Reset gravity
        Debug.Log("Ground Slam Ended!");
    }
}
