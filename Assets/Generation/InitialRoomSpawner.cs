using UnityEngine;

public class InitialSpawnScript : MonoBehaviour
{
    public GameObject[] roomPrefabs;   // Array of room prefabs to pick from
    public int initialRoomCount = 20;    // Number of rooms to spawn at the start
    public float roomWidth = 20f;    // Width of each room (so we know where to place the next one)
    public GameObject roomSpawner;     // Reference to the Room Spawner object

    private float lastSpawnPositionX;  // Keeps track of the last room's spawn position on the X-axis

    void Start()
    {
        // Ensure the room spawner is set to the initial spawn position
        lastSpawnPositionX = roomSpawner.transform.position.x;

        // Spawn the initial rooms
        SpawnInitialRooms();
    }

    // Method to spawn the initial set of rooms
    void SpawnInitialRooms()
    {
        for (int i = 0; i < initialRoomCount; i++)
        {
            SpawnRoom();
        }
    }

    // Method to spawn a single room
    void SpawnRoom()
    {
        // Pick a random room prefab from the array
        GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];

        // Instantiate the room at the next spawn position
        Vector3 spawnPosition = new Vector3(lastSpawnPositionX + roomWidth, roomSpawner.transform.position.y, 0f); // Spawn on the same Y as Room Spawner
        GameObject newRoom = Instantiate(roomPrefab, spawnPosition, Quaternion.identity);

        // Update the last spawn position to the new room's position
        lastSpawnPositionX = newRoom.transform.position.x;
    }
}
