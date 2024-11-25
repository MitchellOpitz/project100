using UnityEngine;

public class ProjectileParticleSystem : MonoBehaviour
{
    [SerializeField] private GameObject enemyDeathParticlePrefab;

    private void OnEnable()
    {
        Projectile.OnProjectileHit += SpawnParticleEffect;
    }

    private void OnDisable()
    {
        Projectile.OnProjectileHit -= SpawnParticleEffect;
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
