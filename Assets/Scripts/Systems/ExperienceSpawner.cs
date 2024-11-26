using UnityEngine;

public class ExperienceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject experiencePrefab; // The prefab for experience
    [SerializeField] private int numberOfOrbs = 3; // Number of orbs to spawn per enemy
    [SerializeField] private float spawnRadius = 1f; // Radius around the enemy to spawn orbs

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += SpawnExperience; // Directly subscribe
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= SpawnExperience; // Unsubscribe
    }

    private void SpawnExperience(int scoreValue, Vector3 position)
    {
        for (int i = 0; i < numberOfOrbs; i++)
        {
            Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = position + new Vector3(randomOffset.x, randomOffset.y, 0); // Adjust for 3D space
            Instantiate(experiencePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
