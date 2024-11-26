using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public SpawnerData[] enemyData; // Array of spawn configurations
    private float[] nextSpawnTimes; // Tracks the next spawn time for each type
    private bool isSpawningActive = true; // Flag to control whether spawning is active

    private void Start()
    {
        nextSpawnTimes = new float[enemyData.Length];
        Countdown.OnCountdownComplete += StartSpawner;
        ExperienceManager.OnLevelUp += StopSpawning; // Subscribe to the level-up event
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        Countdown.OnCountdownComplete -= StartSpawner;
        ExperienceManager.OnLevelUp -= StopSpawning;
    }

    public void StartSpawner()
    {
        // Initialize the spawn times for each enemy type
        for (int i = 0; i < enemyData.Length; i++)
        {
            nextSpawnTimes[i] = Time.time + enemyData[i].spawnRate;
        }
        isSpawningActive = true;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawningActive)
        {
            for (int i = 0; i < enemyData.Length; i++)
            {
                if (Time.time >= nextSpawnTimes[i])
                {
                    SpawnEnemy(enemyData[i]);
                    nextSpawnTimes[i] = Time.time + enemyData[i].spawnRate;
                }
            }
            yield return null;
        }
    }

    private void SpawnEnemy(SpawnerData data)
    {
        // Randomly choose a spawn direction
        SpawnDirection direction = (SpawnDirection)Random.Range(0, 4);

        // Get the spawn position based on the direction
        Vector3 spawnPosition = GetSpawnPosition(direction);

        // Instantiate and configure the enemy
        GameObject enemy = Instantiate(data.enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        if (enemyScript != null)
        {
            enemyScript.speed = data.speed;
            enemyScript.SetSpawnDirection(direction);
        }
    }

    private Vector3 GetSpawnPosition(SpawnDirection direction)
    {
        float x, y;
        float buffer = 1f; // Offset for spawning enemies slightly outside the boundary
        float spawnRange = 5f; // The range within which enemies spawn outside the boundary

        switch (direction)
        {
            case SpawnDirection.Top:
                x = Random.Range(GameBoundary.Instance.GetMinX(), GameBoundary.Instance.GetMaxX());
                y = Random.Range(GameBoundary.Instance.GetMaxY() + buffer, GameBoundary.Instance.GetMaxY() + spawnRange);
                break;
            case SpawnDirection.Bottom:
                x = Random.Range(GameBoundary.Instance.GetMinX(), GameBoundary.Instance.GetMaxX());
                y = Random.Range(GameBoundary.Instance.GetMinY() - spawnRange, GameBoundary.Instance.GetMinY() - buffer);
                break;
            case SpawnDirection.Left:
                x = Random.Range(GameBoundary.Instance.GetMinX() - spawnRange, GameBoundary.Instance.GetMinX() - buffer);
                y = Random.Range(GameBoundary.Instance.GetMinY(), GameBoundary.Instance.GetMaxY());
                break;
            case SpawnDirection.Right:
                x = Random.Range(GameBoundary.Instance.GetMaxX() + buffer, GameBoundary.Instance.GetMaxX() + spawnRange);
                y = Random.Range(GameBoundary.Instance.GetMinY(), GameBoundary.Instance.GetMaxY());
                break;
            default:
                x = GameBoundary.Instance.GetMinX();
                y = GameBoundary.Instance.GetMinY();
                break;
        }

        return new Vector3(x, y, 0);
    }

    // Method to stop the spawner when the player levels up
    private void StopSpawning(int level)
    {
        Debug.Log("Level up! Stopping enemy spawner.");
        isSpawningActive = false; // Disable further spawning

        // Find and destroy all enemies
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in allEnemies)
        {
            enemy.DestroyEnemy(); // Call DestroyEnemy() on each enemy
        }
    }
}