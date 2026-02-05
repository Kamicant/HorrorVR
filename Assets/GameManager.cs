using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int numberOfCollectibles = 10;
    public float spawnRadius = 8f;

    public GameObject speedZonePrefab;
    public int numberOfSpeedZones = 3;

    void Start()
    {
        SpawnCollectibles();
        SpawnSpeedZones();
    }

    void SpawnCollectibles()
    {
        // Loop to create multiple collectibles
        for (int i = 0; i < numberOfCollectibles; i++)
        {
            // Generate random position
            float randomX = Random.Range(-spawnRadius, spawnRadius);
            float randomZ = Random.Range(-spawnRadius, spawnRadius);
            Vector3 spawnPosition = new Vector3(randomX, 1f, randomZ);

            // Create the collectible at that position
            Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
        }

        Debug.Log("Spawned " + numberOfCollectibles + " collectibles!");
    }

    void SpawnSpeedZones()
    {
        for (int i = 0; i < numberOfSpeedZones; i++)
        {
            // Calculate positions in a circle pattern
            float angle = i * (360f / numberOfSpeedZones);
            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * spawnRadius;
            float z = Mathf.Sin(radians) * spawnRadius;

            Vector3 position = new Vector3(x, 0.1f, z);

            Instantiate(speedZonePrefab, position, Quaternion.identity);
        }

        Debug.Log("Spawned " + numberOfSpeedZones + " speed zones!");
    }
}