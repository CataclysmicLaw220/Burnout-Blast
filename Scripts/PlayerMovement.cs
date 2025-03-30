using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float crouchSpeed = 2.5f; // Reduced speed when crouching
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb; // hi
    private bool isGrounded;
    private bool isCrouching;
    private float originalHeight;
    private CapsuleCollider2D playerCollider;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        originalHeight = playerCollider.size.y;
    }

    void Update() {
    // Check if player is on the ground
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    // Get movement input
    float moveInput = Input.GetAxisRaw("Horizontal");

    // Check for crouch input
    isCrouching = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl);

    // Apply movement
    float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
    rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);

    // Adjust gravity when crouching
    rb.gravityScale = isCrouching ? 7f : 2.5f; // Set gravity to 7 when crouching, reset to 2.5 otherwise

    // Jumping
    if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded) {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    // Adjust collider when crouching
    if (isCrouching) {
        playerCollider.size = new Vector2(playerCollider.size.x, originalHeight * 0.5f);
    } else {
        playerCollider.size = new Vector2(playerCollider.size.x, originalHeight);
    }
}
}
