using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0f;
    public float shakeIntensity = 0.1f;
    public float shakeFrequency = 5f;

    private Vector3 originalPosition;
    private PlayerHealth playerHealth; // Reference to the PlayerHealth instance

    private void Start()
    {
        originalPosition = transform.position;

        // Find PlayerHealth instance in the scene
        playerHealth = FindObjectOfType<PlayerHealth>();

        // Subscribe to the events using lambdas
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged += _ => TriggerShake(0.5f, 0.3f);
            playerHealth.OnPlayerDeath += () => TriggerShake(0.5f, 0.5f);
        }

        // Subscribe to Enemy event
        Enemy.OnEnemyKilled += (score, position) => TriggerShake(0.3f, 0.2f);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the events when this object is destroyed
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged -= _ => TriggerShake(0.3f, 0.2f);
            playerHealth.OnPlayerDeath -= () => TriggerShake(0.5f, 0.5f);
        }
        Enemy.OnEnemyKilled -= (score, position) => TriggerShake(0.5f, 0.3f);
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;

            // Generate random shake position
            float shakeX = Random.Range(-1f, 1f) * shakeIntensity;
            float shakeY = Random.Range(-1f, 1f) * shakeIntensity;

            // Apply shake effect
            transform.position = new Vector3(originalPosition.x + shakeX, originalPosition.y + shakeY, originalPosition.z);

            // Smooth out the shake over time
            if (shakeDuration <= 0)
            {
                transform.position = originalPosition;
            }
        }
    }

    public void TriggerShake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
    }
}
