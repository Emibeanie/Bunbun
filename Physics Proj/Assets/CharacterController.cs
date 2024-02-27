using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject Button;
    public float constantSpeed = 7f;

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

        transform.Translate(Vector2.left * Time.deltaTime * velocity);

        if (Input.GetKey(KeyCode.Space))
            transform.Translate(Vector2.up * Time.deltaTime * velocity);
    }

    private void ApplyGravity()
    {
        Vector2 gravityForce = gravity * mass;
        Vector2 acceleration = gravityForce / mass;
        velocity += acceleration * Time.deltaTime;
    }


}
