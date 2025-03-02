using UnityEngine;
using System.Collections;  // Required for IEnumerator

public class FireBoxTrap : MonoBehaviour
{
    public float damage = 10f;  // Amount of damage dealt by the trap
    public Vector2 damageAreaSize = new Vector2(3f, 1f);  // Size of the box-shaped damage area (width, height)
    public float range = 5f;    // Range of the damaging area above the box (height)
    public float delayTime = 1f; // Delay before the trap activates after trigger
    public float damageDuration = 2f; // Duration for the damaging area

    public GameObject fireEffectPrefab;  // A prefab of the fire effect (optional for visual)

    private bool isTriggered = false;

    // When the player enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            Invoke("ActivateTrap", delayTime);  // Delays trap activation
        }
    }

    // Activate the trap after the delay
    private void ActivateTrap()
    {
        // Visual effect (fire shooting above the box)
        if (fireEffectPrefab)
        {
            Instantiate(fireEffectPrefab, transform.position + Vector3.up * range, Quaternion.identity);
        }

        // Apply damage over the duration using StartCoroutine
        StartCoroutine(DamageArea());
    }

    // Handle the damaging area (now in box shape)
    private IEnumerator DamageArea()
    {
        // Set the damage area above the box (this is the position of the box where damage occurs)
        Vector3 areaPosition = transform.position + Vector3.up * range;

        // Use OverlapBox2D for 2D physics to define a box-shaped damage area
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(areaPosition, damageAreaSize, 0f); // Box dimensions

        foreach (Collider2D hitCollider in hitColliders)
        {
            // Apply damage if the object has a health script
            if (hitCollider.CompareTag("Player"))
            {
                // Apply damage (assuming the player has a "PlayerHealth" script)
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);  // Call TakeDamage from PlayerHealth
                }
            }
        }

        // Wait for the duration of the damage effect
        yield return new WaitForSeconds(damageDuration);

        isTriggered = false;  // Reset the trap for future use
    }

    // Optionally, visualize the area of effect for debugging purposes (in the Editor)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw a rectangle to visualize where the flame would reach (box-shaped damage area)
        Vector3 areaPosition = transform.position + Vector3.up * range;
        Gizmos.DrawWireCube(areaPosition, new Vector3(damageAreaSize.x, damageAreaSize.y, 0f));  // Box-shaped damage area
    }
}
