using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;  // For using UI Text components

public class player_Movement : MonoBehaviour
{
    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rb;

    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    [Header("UI")]
    public TMP_Text scoreText;  // Reference to the Score UI Text
    

    private float horizontal;
    private float lastScorePositionX; // Store the last position where we updated the score
    private float scoreThreshold = 20.83f; // Distance after which the score should increase
    private int score = 0; // Player's score

    #region PLAYER_CONTROLS
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded()) // Only jump if on the ground
        {
            // Apply jumping force by modifying the vertical velocity
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    private bool isGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(transform.position - new Vector3(0, 1f, 0), 0.2f, LayerMask.GetMask("Ground"));
        return grounded;
    }
    #endregion

    private void FixedUpdate()
    {
        // Apply movement while keeping the vertical velocity unchanged
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Check if the player has moved enough distance to update the score
        if (transform.position.x - lastScorePositionX >= scoreThreshold)
        {
            // Increase the score
            score++;
            // Update the score text UI
            scoreText.text = "Score: " + score.ToString();

            // Print the current score to the console
            Debug.Log("Player Score: " + score);

            // Update the last score position to the current x position
            lastScorePositionX = transform.position.x;
        }

        // Update the health text UI
        
    }

    // Debug the ground check visually in the editor
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.4f); // Red circle for ground check area
        }
    }

   

    // Example method to heal the player (you can call this when the player heals)
    
}
