using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Normal physics")]
    public float normalMovementSpeed;
    public float jumpHeight;
    float verticalSpeed;
    Vector2 movement;
    float fallMultiplier = 1;
    bool isGrounded, isWallAhead;
    
    [Header("Sand Area")]
    public float sandMovementSpeedMultiplier = 0.5f;

    [Header("Windy Area")]
    public float windStrength;

    [Header("Low Gravity Area")]
    public Collider2D lowGravityArea;
    public float lowGravityMultiplier;

    [Header("Components")]
    public Transform groundCheckRayPoint, horizontalCheckRayPoint;
    public LayerMask groundLayer;
    public GameObject startPanel;
    bool gameStarted = false;
    

    void Update()
    {
        CheckGround();
        CheckJumping();
        CheckHorizontalCollision();
        ApplyMovement();
        EndZone();

        Debug.Log(movement);
    }

    void CheckGround()
    {
        RaycastHit2D _hit = Physics2D.Raycast(groundCheckRayPoint.position, -Vector2.up, 0.2f, groundLayer);
        if (_hit.collider)
        {
            verticalSpeed = 0;
            isGrounded = true;
            fallMultiplier = 1;
        }
        else
            verticalSpeed = Mathf.Lerp(verticalSpeed, -10 * fallMultiplier, 1f * Time.deltaTime);
    }

    void CheckJumping()
    {
        if ((isGrounded || fallMultiplier < 1) && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            fallMultiplier = 1;
            verticalSpeed = jumpHeight;
        }
    }

    void CheckHorizontalCollision()
    {
        RaycastHit2D _hit = Physics2D.Raycast(horizontalCheckRayPoint.position, Vector2.right * Mathf.Sign(transform.localScale.x), 0.2f, groundLayer);
        if (_hit.collider)
        {
            isWallAhead = true;
            transform.position -= new Vector3((0.2f - _hit.distance) * Mathf.Sign(transform.localScale.x), 0);
        }
        else
        {
            isWallAhead = false;
        }
    }

    void ApplyMovement()
    {
        if (isWallAhead)
            normalMovementSpeed = 0;

        if (isInWidyArea())
        {
            // Randomly determine direction (left or right)
            float randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;

            // Calculate wind force (in horizontal direction)
            float windForce = randomDirection * windStrength * Time.deltaTime;

            // Apply wind force to the player's horizontal movement
            normalMovementSpeed += windForce;
            Debug.Log(normalMovementSpeed);
        }

        if (isInLowGravityArea())
        {
            // Reduce the vertical speed to simulate lower gravity
            verticalSpeed *= lowGravityMultiplier;
        }

        float currentMovementSpeed = isOnSand() ? normalMovementSpeed * sandMovementSpeedMultiplier : normalMovementSpeed;

        movement = new Vector2(normalMovementSpeed, verticalSpeed);

        if(gameStarted)
        transform.Translate(movement * currentMovementSpeed * Time.deltaTime);
    }

    bool isOnSand()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Sand"))
            {
                Debug.Log("in sandy area");
                return true;
            }
                
        }
        return false;
    }
    bool isInWidyArea()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("WindyArea"))
            {
                Debug.Log("in windy area");
                return true;
            }
        }
        return false;
    }

    bool isInLowGravityArea()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("GravityArea"))
            {
                Debug.Log("in gravity area");
                return true;
            }
        }
        return false;
    }
    bool isStopped() ///////
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Obstacle"))
            {
                Debug.Log("Hit Obstacle");
                return true;
            }
        }
        return false;
    }

    bool isInEnd()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("End"))
            {
                Debug.Log("in End area");
                return true;
            }
        }
        return false;
    }

    void EndZone()
    {
        if (isInEnd())
        {
            transform.position = new Vector2(-16.05016f, -4.9f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(groundCheckRayPoint.position, -Vector2.up * 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(horizontalCheckRayPoint.position, Vector2.right * 0.2f * Mathf.Sign(transform.localScale.x));
    }

    public void StartButton()
    {
        gameStarted = true;
        startPanel.SetActive(false);
    }
}