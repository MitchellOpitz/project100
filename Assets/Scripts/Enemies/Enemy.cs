using System;
using UnityEngine;
using FMODUnity;

public abstract class Enemy : MonoBehaviour
{
    // Update the event to include position
    public static event Action<int, Vector3> OnEnemyKilled;
    public static event Action<Vector3, Color> OnEnemyDeath;

    public float health = 3;  // Default health for the enemy
    public float speed;
    public int scoreValue = 100;

    public abstract void Move();

    public void TakeDamage(float damage)
    {
        Debug.Log($"Damage taken: {damage}");
        health -= damage;
        Debug.Log($"New health value: {health}");

        // Check if the enemy is dead
        if (health <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        Move();

        if (GameBoundary.Instance != null && GameBoundary.Instance.IsOutOfBounds(transform.position, 5f))
        {
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        // Use the cached multiplier rank from ScoreManager
        int scoreMultiplierRank = UpgradeManager.GetUpgradeRank("Score Multiplier");
        Debug.Log($"Cached Score Multiplier Rank used in Die: {scoreMultiplierRank}");

        int finalScoreValue = Mathf.RoundToInt(scoreValue * (1 + scoreMultiplierRank * 0.25f));
        Debug.Log($"ScoreValue: {scoreValue}, MultiplierRank: {scoreMultiplierRank}, FinalScoreValue: {finalScoreValue}");

        OnEnemyKilled?.Invoke(finalScoreValue, transform.position);
        DestroyEnemy();
    }

    public void DestroyEnemy()
    {
        // Trigger instance-specific death event
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color particleColor = spriteRenderer != null ? spriteRenderer.color : Color.white;
        OnEnemyDeath?.Invoke(transform.position, particleColor);

        // Destroy the enemy object
        RuntimeManager.PlayOneShot("event:/EnemyDefeated");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only trigger damage if the collider is the player
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();  // Only call TakeDamage here
            }
            Destroy(gameObject);
        }
    }

    public void SetSpawnDirection(SpawnDirection direction)
    {
        switch (direction)
        {
            case SpawnDirection.Top:
                transform.rotation = Quaternion.Euler(0, 0, 180); // Rotate as needed
                break;
            case SpawnDirection.Bottom:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case SpawnDirection.Left:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case SpawnDirection.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }
}
