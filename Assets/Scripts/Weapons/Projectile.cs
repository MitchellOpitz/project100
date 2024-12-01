using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static event Action<Vector3, Color> OnProjectileHit;

    public float speed = 10f;
    public float boundaryOffset = 10f;  // The offset for when the projectile goes out of bounds
    public float damage = 1f;

    private int damageMultiplierRank;
    private Rigidbody2D rb;
    private GameBoundary gameBoundary;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;  // Move the projectile in the direction it's facing
        gameBoundary = GameBoundary.Instance;  // Use GameBoundary singleton
        damageMultiplierRank = UpgradeManager.GetUpgradeRank("Damage Multiplier");
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
                float finalDamageValue = damage * (1 + ((float)damageMultiplierRank * .10f));
                Debug.Log($"Final damage value: {finalDamageValue}");
                enemy.TakeDamage(finalDamageValue);
                
                // Emit an event for particle effects
                SpriteRenderer bulletSprite = GetComponent<SpriteRenderer>();
                Color particleColor = bulletSprite != null ? bulletSprite.color : Color.white;
                OnProjectileHit?.Invoke(transform.position, particleColor);
            }

            Destroy(gameObject);  // Destroy the projectile after hitting an enemy
        }
    }
}
