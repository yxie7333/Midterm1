using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public int pelletsToCollectForPhase = 2;
    public float phaseDuration = 2.0f;
    

    private Rigidbody2D rb;
    private int totalPelletsCollected = 0;
    private int currentPelletsCanUse = 0;
    private bool canPhase = false;
    private int originalLayer;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalLayer = gameObject.layer; // Pac-Man's original layer
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize the SpriteRenderer
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.Playing)
        {
            // Movement
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 moveVelocity = moveInput.normalized * speed;
            rb.velocity = moveVelocity;

            // Phase ability activation
            if (canPhase && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(PhaseThroughWalls());
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.tag);
        if (other.CompareTag("Pellet"))
        {
            totalPelletsCollected++;
            currentPelletsCanUse++;
            Destroy(other.gameObject); // remove the pellet
            Debug.Log("Total" + totalPelletsCollected);
            Debug.Log("Current" + currentPelletsCanUse);
            if (currentPelletsCanUse >= pelletsToCollectForPhase)
            {
                canPhase = true;
            }
        }

        if (other.CompareTag("Apple"))
        {
            // Player has consumed the apple and wins the game.
            GameManager.instance.PlayerWon();
            Destroy(other.gameObject); // Destroy the apple
        }
    }

    // End the phase ability after 2 sec.
    public void EndPhase()
    {
        canPhase = false;
        // Re-enable wall collisions or any other post-phase logic here.
    }

    private IEnumerator PhaseThroughWalls()
    {
        Color tempColor = originalColor;
        tempColor.a = 0.5f; // Set alpha to 0.5 to make it half-transparent
        spriteRenderer.color = tempColor;

        gameObject.layer = LayerMask.NameToLayer("Phasing"); // Change to the Phasing layer to avoid wall collisions
        yield return new WaitForSeconds(phaseDuration);
        gameObject.layer = originalLayer; // Return to the original layer after phaseDuration
        spriteRenderer.color = originalColor;
        canPhase = false; // Reset phase ability after use
        currentPelletsCanUse -= pelletsToCollectForPhase;
        
    }


}
