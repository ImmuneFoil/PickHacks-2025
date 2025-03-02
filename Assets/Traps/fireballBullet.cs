using UnityEngine;

public class FireballBullet : MonoBehaviour
{
    public float speed = 10f;  // Speed of the fireball
    public float damage = 20f; // Damage dealt to the player

    private void Start()
    {
        // Ensure the fireball has a Rigidbody2D attached for movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(speed, 0f);  // Fire the fireball horizontally (right direction)
        }
    }

    // Trigger event when the fireball overlaps with something
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the fireball hit the player
        if (other.CompareTag("Player"))
        {
            // Apply damage to the player (assuming the player has a "PlayerHealth" script)
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);  // Apply damage to the player
                Debug.Log("Player took damage: " + damage);  // Log the damage dealt
            }

            // Destroy the fireball after it collides with the player
            Destroy(gameObject);
        }

        // Optionally, destroy the fireball if it hits any other object (e.g., wall)
        // For example:
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject); // Destroy fireball if it hits an obstacle
        }
    }

    // Optional: Visualize the fireball's collision radius in the Editor (for debugging)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);  // Draw a small sphere to show the fireball's radius
    }
}
