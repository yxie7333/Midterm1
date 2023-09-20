using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player;  // Reference to Pac-Man's transform.
    public float speed = 5.0f;  // Speed of the ghost's movement.
    public float chaseThreshold = 5.0f;  // Distance at which the ghost starts chasing the player.

    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private Vector2 currentDirection;

    private Vector2 previousPosition;
    private float timeSinceLastCheck = 0.0f;
    private float checkTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        ChooseRandomDirection();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.Playing)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer < chaseThreshold)
            {
                // Chase the player.
                Vector2 chaseDirection = (player.position - transform.position).normalized;
                transform.Translate(chaseDirection * speed * Time.deltaTime);
            }
            else
            {
                // Move in the current random direction.
                transform.Translate(currentDirection * speed * Time.deltaTime);
            }

            // Check if the ghost is stuck
            timeSinceLastCheck += Time.deltaTime;
            if (timeSinceLastCheck > checkTime)
            {
                float distanceMoved = Vector2.Distance(transform.position, previousPosition);
                if (distanceMoved < 0.1f)
                {
                    ChooseRandomDirection(); // If the ghost hasn't moved much, change direction
                }

                previousPosition = transform.position; // Update the previous position
                timeSinceLastCheck = 0.0f; // Reset the timer
            }
        }
    }

    private void ChooseRandomDirection()
    {
        currentDirection = directions[Random.Range(0, directions.Length)];
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Ghost collided with: " + col.gameObject.name);
        if (col.gameObject.CompareTag("Walls"))
        {
            ChooseRandomDirection();
        }

        // Check collision with player to end the game
        if (col.gameObject.CompareTag("Player"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        GameManager.instance.GameOver();
    }
}
