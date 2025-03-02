using UnityEngine;

public class SawBladeScript : MonoBehaviour
{
    public float moveDistance = 5f;  // The distance the sawblade will travel left and right
    public float speed = 3f;         // Speed of the sawblade's movement
    public float damage = 20f;       // Damage dealt to the player
    public Vector2 damageAreaSize = new Vector2(1f, 1f);  // Size of the box-shaped damage area (width, height)

    private Vector3 startPosition;   // The starting position of the sawblade
    private bool movingTowardsRight = true; // Flag to track which direction the sawblade is moving

    private void Start()
    {
        // Store the initial position of the sawblade as the starting point
        startPosition = transform.position;
    }

    private void Update()
    {
        // Move the sawblade back and forth based on the distance
        MoveSawblade();

        // Continuously check for player in the damage area while moving
        ApplyDamageArea();
    }

    private void MoveSawblade()
    {
        if (movingTowardsRight)
        {
            // Move towards the right (startPosition + moveDistance)
            transform.position = Vector3.MoveTowards(transform.position, startPosition + Vector3.right * moveDistance, speed * Time.deltaTime);

            // Check if the sawblade reached the rightmost point
            if (Vector3.Distance(transform.position, startPosition + Vector3.right * moveDistance) < 0.1f)
            {
                movingTowardsRight = false;
            }
        }
        else
        {
            // Move towards the left (startPosition - moveDistance)
            transform.position = Vector3.MoveTowards(transform.position, startPosition - Vector3.right * moveDistance, speed * Time.deltaTime);

            // Check if the sawblade reached the leftmost point
            if (Vector3.Distance(transform.position, startPosition - Vector3.right * moveDistance) < 0.1f)
            {
                movingTowardsRight = true;
            }
        }
    }

    // Continuously check for the player within the sawblade's damage area
    private void ApplyDamageArea()
    {
        // Set the damage area (above or below the sawblade depending on your setup)
        Vector3 areaPosition = transform.position + Vector3.up * 0.5f; // Slightly offset to avoid hitting the blade itself

        // Use OverlapBox2D to detect any colliders within the damage area
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(areaPosition, damageAreaSize, 0f); // Box dimensions

        foreach (Collider2D hitCollider in hitColliders)
        {
            // Apply damage if the object has a health script and is the player
            if (hitCollider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);  // Apply damage to the player
                }
            }
        }
    }

    // Optional: Visualize the damage area for debugging purposes in the Editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw the damage area as a box in the editor for visualization
        Vector3 areaPosition = transform.position + Vector3.up * 0.5f;  // Offset to avoid the sawblade itself
        Gizmos.DrawWireCube(areaPosition, new Vector3(damageAreaSize.x, damageAreaSize.y, 0f));  // Box-shaped damage area
    }
}
