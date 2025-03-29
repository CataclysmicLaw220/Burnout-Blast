using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Homing : MonoBehaviour {
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public int damage = 1; // Damage per hit

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (target == null) return;

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null) {
                player.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy missile on impact
        }
    }
}
