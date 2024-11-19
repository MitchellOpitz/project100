using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health = 3;  // Default health for the enemy

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

    // Method to handle the enemy's death
    private void Die()
    {
        // You can add death animations or effects here
        Debug.Log(gameObject.name + " has been destroyed!");

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
}
