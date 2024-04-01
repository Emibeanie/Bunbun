using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheck : MonoBehaviour
{
    public Vector2 targetPosition; // Target position to move the player

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform playerTransform = collision.gameObject.transform;

            playerTransform.position = new Vector2(targetPosition.x, targetPosition.y);
        }
    }
}
