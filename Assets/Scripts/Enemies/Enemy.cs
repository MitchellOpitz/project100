using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static event Action<int> OnEnemyKilled;
    public int health = 3;  // Default health for the enemy
    public float speed;
    public int scoreValue = 100;
    public abstract void Move();

    // Method to handle taking damage
    public void TakeDamage()
    {
        health -= 1;

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

    // Method to handle the enemy's death
    private void Die()
    {
        OnEnemyKilled?.Invoke(scoreValue);

        // Destroy the enemy object
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
