using UnityEngine;

[System.Serializable]
public class SpawnerData
{
    public GameObject enemyPrefab; // Single enemy type
    public float spawnRate; // Unique spawn rate for this type
    public float speed; // Unique speed for this type
    public float health; // Unique health for this type
}
