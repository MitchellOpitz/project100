using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveData[] waves; // Define all wave changes in the inspector
    private int currentWaveIndex = 0;

    public EnemySpawner enemySpawner; // Reference to the EnemySpawner

    public delegate void WaveChanged(int waveIndex);
    public static event WaveChanged OnWaveChanged;

    private void OnEnable()
    {
        // Subscribe to the OnUpgradeSelected event
        UpgradeManager.OnUpgradeSelected += StartNextWave;
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnUpgradeSelected event
        UpgradeManager.OnUpgradeSelected -= StartNextWave;
    }

    private void Start()
    {
        StartNextWave(); // Start the first wave
    }

    public void StartNextWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves completed!");
            StartInfiniteScaling();
            return; // No more waves to start
        }

        Debug.Log($"Starting Wave {currentWaveIndex + 1}");

        // Apply changes from the current wave
        ApplyWaveChanges(waves[currentWaveIndex]);

        // Notify listeners that the wave has changed
        OnWaveChanged?.Invoke(currentWaveIndex);

        // Increment the wave index for the next call
        currentWaveIndex++;
    }

    private void ApplyWaveChanges(WaveData wave)
    {
        foreach (SpawnerData change in wave.spawnerChanges)
        {
            // Find the corresponding enemy type in the spawner
            for (int i = 0; i < enemySpawner.enemyData.Length; i++)
            {
                SpawnerData spawnerData = enemySpawner.enemyData[i];
                if (spawnerData.enemyPrefab == change.enemyPrefab)
                {
                    // Apply changes (non-zero values in WaveData override existing values)
                    if (change.spawnRate > 0) spawnerData.spawnRate = change.spawnRate;
                    if (change.speed > 0) spawnerData.speed = change.speed;
                    if (change.health > 0) spawnerData.health = change.health;
                }
            }
        }
    }
    private void StartInfiniteScaling()
    {
        Debug.Log("Infinite scaling activated!");

        // Scale enemy attributes by 5%
        foreach (SpawnerData spawnerData in enemySpawner.enemyData)
        {
            spawnerData.spawnRate *= 0.95f; // Increase spawn rate by 5%
            spawnerData.speed *= 1.05f; // Increase speed by 5%
            spawnerData.health *= 1.05f; // Increase health by 5%
        }

        // Notify listeners that the wave has changed (optional: to indicate infinite scaling)
        OnWaveChanged?.Invoke(currentWaveIndex);

        // Trigger the next infinite wave
        currentWaveIndex++; // Increment wave index for clarity
        Debug.Log($"Infinite Wave {currentWaveIndex} scaling applied.");
    }
}
