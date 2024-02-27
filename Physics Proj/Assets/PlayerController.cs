using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float constantSpeed = 7f;
    public float jumpForce = 10f;
    private bool isGrounded = true; // Track if the character is grounded or not

    Vector2 gravity = new Vector2(0, -9.8f);
    Vector2 velocity;
    float mass = 0.5f;

    private void Start()
    {
        velocity += (Vector2)transform.position * constantSpeed;
    }

    private void FixedUpdate()
    {
        ApplyGravity();

        // Horizontal movement
        transform.Translate(Vector2.left * Time.deltaTime * velocity.x);

        // Vertical movement
        if (Input.GetKey(KeyCode.Space) && isGrounded) // Only jump when grounded
        {
            Jump();
        }
        else
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocity.y);
        }
    }

    private void ApplyGravity()
    {
        Vector2 gravityForce = gravity * mass;
        Vector2 acceleration = gravityForce / mass;
        velocity += acceleration * Time.deltaTime;
    }

    private void Jump()
    {
        velocity.y = jumpForce; // Apply jump force
        isGrounded = false; // Set grounded to false
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the character collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set grounded to true
        }
    }
}
