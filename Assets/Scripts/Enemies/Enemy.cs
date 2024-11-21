using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health = 3;  // Default health for the enemy
    public float speed;
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
