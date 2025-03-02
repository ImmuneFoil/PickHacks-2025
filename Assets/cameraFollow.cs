using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 10, -10);  // Camera offset

    void Update()
    {
        if (player != null)
        {
            // Update the camera's position, locking the Z-axis
            Vector3 desiredPosition = player.position + offset;
            desiredPosition.z = transform.position.z;  // Lock the Z-axis

            // Apply the new position to the camera
            transform.position = desiredPosition;
        }
    }
}
