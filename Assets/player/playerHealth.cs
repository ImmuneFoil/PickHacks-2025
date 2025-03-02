using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Starting health value
    public TMP_Text healthText;  // Reference to the Health UI Text
    public GameOverScript gameOverScript; // Reference to the GameOverScript

    private bool isDead = false; // Flag to check if the player is dead

    void Update()
    {
        // Update the health UI in case of damage
        healthText.text = "Health: " + health.ToString();
    }

    // Call this method to apply damage to the player
    public void TakeDamage(float damage)
    {
        if (isDead) return;  // Don't take damage if already dead

        health -= damage;
        Debug.Log("Player took damage! Health: " + health);
        if (health <= 0f && !isDead)
        {
            Die();
        }
    }

    // Handle player death
    private void Die()
    {
        Debug.Log("Player died");
        isDead = true;

        // Activate the Game Over screen
        if (gameOverScript != null)
        {
            gameOverScript.GameOver();
            
        }

        // Stop the player from moving by disabling player controls
        var playerMovement = GetComponent<player_Movement>();  // Assumes you have a player_Movement script
        if (playerMovement != null)
        {
            playerMovement.enabled = false;  // Disables the player's movement script
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
