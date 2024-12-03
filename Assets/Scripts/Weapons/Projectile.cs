using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static event Action<Vector3, Color> OnProjectileHit;

    public float speed = 10f;
    public float boundaryOffset = 10f;  // The offset for when the projectile goes out of bounds
    public float damage = 1f;
    public float baseCritChance = 10f;

    private int damageMultiplierRank;
    private int critMultiplierRank;
    private int critChanceRank;
    private int pierceChanceRank;

    private int maxPierce;
    private int currentPierce;
    private Rigidbody2D rb;
    private GameBoundary gameBoundary;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;  // Move the projectile in the direction it's facing
        gameBoundary = GameBoundary.Instance;  // Use GameBoundary singleton
        damageMultiplierRank = UpgradeManager.GetUpgradeRank("Damage Multiplier");
        critMultiplierRank = UpgradeManager.GetUpgradeRank("Crit Multiplier");
        critChanceRank = UpgradeManager.GetUpgradeRank("Critical Chance");
        pierceChanceRank = UpgradeManager.GetUpgradeRank("Pierce Chance");
        maxPierce = UpgradeManager.GetUpgradeRank("Pierce Number");
        currentPierce = 0;
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
                float criticalStrikeChance = (baseCritChance + (critChanceRank * 5f));
                Debug.Log($"crit chance: {criticalStrikeChance}");
                bool criticalStrike = UnityEngine.Random.Range(0f, 100f) < (baseCritChance + (critChanceRank * 5f));
                float finalDamageValue = damage * (1 + (float)damageMultiplierRank * .10f);
                if (criticalStrike)
                {
                    finalDamageValue = damage * (1 + (float)critMultiplierRank * .10f);
                    Debug.Log($"Critical strike! {finalDamageValue}");
                }
                Debug.Log($"Final damage value: {finalDamageValue}");
                enemy.TakeDamage(finalDamageValue);
                
                // Emit an event for particle effects
                SpriteRenderer bulletSprite = GetComponent<SpriteRenderer>();
                Color particleColor = bulletSprite != null ? bulletSprite.color : Color.white;

                OnProjectileHit?.Invoke(transform.position, particleColor);
            }

            bool pierceStrike = UnityEngine.Random.Range(0f, 100f) < (pierceChanceRank * 10f);
            if (pierceStrike && currentPierce < maxPierce)
            {
                currentPierce++;
                return;
            }
            else
            {
                Destroy(gameObject);  // Destroy the projectile after hitting an enemy
            }
        }
    }
}
