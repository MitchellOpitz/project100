using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public SpawnerData[] enemyData;
    private float nextSpawnTime;

    private void Start()
    {
        StartSpawner();
        Debug.Log("Spawnere started");
    }

    public void StartSpawner()
    {
        // Start the spawning routine based on the configuration data
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            foreach (var data in enemyData)
            {
                if (Time.time >= nextSpawnTime)
                {
                    SpawnEnemy(data);
                    nextSpawnTime = Time.time + data.spawnRate;
                }
            }
            yield return null;
        }
    }

    private void SpawnEnemy(SpawnerData data)
    {
        // Choose a random spawn location or fixed direction
        Vector3 spawnPosition = GetRandomSpawnPosition();

        GameObject enemy = Instantiate(data.enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        // Set the enemy's speed and direction
        enemyScript.speed = data.speed;
        //  enemyScript.SetMovementDirection(data.spawnDirection);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Return a spawn position based on the wave or predefined points (top, bottom, etc.)
        // You can return random positions or fixed ones (e.g., top, bottom)
        return new Vector3(-3, 3, 0); // Example
    }
}
