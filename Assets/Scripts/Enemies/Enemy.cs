using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health = 3;  // Default health for the enemy
    public float speed;
    private Vector3 movementDirection;
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
    public void SetSpawnDirection(SpawnDirection direction)
    {
        switch (direction)
        {
            case SpawnDirection.Top:
                movementDirection = Vector3.down; // Move downward
                transform.rotation = Quaternion.Euler(0, 0, -90); // Rotate as needed
                break;
            case SpawnDirection.Bottom:
                movementDirection = Vector3.up; // Move upward
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case SpawnDirection.Left:
                movementDirection = Vector3.right; // Move right
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case SpawnDirection.Right:
                movementDirection = Vector3.left; // Move left
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }
}
