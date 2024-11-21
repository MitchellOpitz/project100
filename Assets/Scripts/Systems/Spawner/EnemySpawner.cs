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
        SpawnDirection direction = (SpawnDirection)Random.Range(0, 4); // Randomly choose a direction
        Vector3 spawnPosition = GetSpawnPosition(direction);

        GameObject enemy = Instantiate(data.enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.speed = data.speed;
        enemyScript.SetSpawnDirection(direction); // Pass the direction to the enemy
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
                // Default spawn position if something goes wrong
                x = GameBoundary.Instance.GetMinX();
                y = GameBoundary.Instance.GetMinY();
                break;
        }

        return new Vector3(x, y, 0);
    }

}
