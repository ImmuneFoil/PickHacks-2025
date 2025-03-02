using UnityEngine;

public class FireballTrap : MonoBehaviour
{
    public GameObject fireballPrefab;  // The fireball prefab
    public float fireRate = 2f;  // The rate at which the fireballs are fired (in seconds)
    public float fireballOffset = 1f;  // The offset in position to shoot the fireball (in case you want it above or in front of the trap)
    public float fireballSpeed = 10f;  // The speed of the fireball

    private void Start()
    {
        // Start shooting fireballs periodically
        InvokeRepeating("ShootFireball", 0f, fireRate);
    }

    // This method will be called periodically to shoot a fireball
    private void ShootFireball()
    {
        // Instantiate the fireball prefab at the trap's position with the specified offset
        GameObject fireball = Instantiate(fireballPrefab, transform.position + Vector3.right * fireballOffset, Quaternion.identity);

        // Set the speed of the fireball by modifying its Rigidbody2D component
        FireballBullet fireballScript = fireball.GetComponent<FireballBullet>();
        if (fireballScript != null)
        {
            fireballScript.speed = fireballSpeed;
        }
    }
}
