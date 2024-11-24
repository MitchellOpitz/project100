using UnityEngine;

public class EnemyParticleSystem : MonoBehaviour
{
    [SerializeField] private GameObject enemyDeathParticlePrefab;

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += SpawnParticleEffect;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= SpawnParticleEffect;
    }

    private void SpawnParticleEffect(Vector3 position, Color color)
    {
        if (enemyDeathParticlePrefab == null) return;

        GameObject particleObj = Instantiate(enemyDeathParticlePrefab, position, Quaternion.identity);
        var particleSystem = particleObj.GetComponent<ParticleSystem>();

        if (particleSystem != null)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = color;

            Destroy(particleObj, mainModule.duration + mainModule.startLifetime.constantMax);
        }
    }
}
