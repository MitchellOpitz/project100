using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float boundaryOffset = 10f;  // The offset for when the projectile goes out of bounds

    private Rigidbody2D rb;
    private GameBoundary gameBoundary;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;  // Move the projectile in the direction it's facing
        gameBoundary = GameBoundary.Instance;  // Use GameBoundary singleton
    }

    void Update()
    {
        // Destroy the projectile if it goes out of bounds, considering the offset
        if (gameBoundary.IsOutOfBounds(transform.position, boundaryOffset))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Handle damage to enemy
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Call a method on the enemy to deal damage
                enemy.TakeDamage();
            }

            Destroy(gameObject);  // Destroy the projectile after hitting an enemy
        }
    }
}
