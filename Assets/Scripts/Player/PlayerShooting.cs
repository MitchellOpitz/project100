using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;  // Delay between shots when holding down fire button
    private int bulletsPerShot = 1;

    private float nextShotTime = 0f;

    private void OnEnable()
    {
        // Subscribe to the OnUpgradeSelected event when this script is enabled
        UpgradeManager.OnUpgradeSelected += OnUpgradeSelected;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event when this script is disabled
        UpgradeManager.OnUpgradeSelected -= OnUpgradeSelected;
    }

    void Update()
    {
        // Shoot when fire button is held down (spacebar in this case) and fire rate condition is met
        if (Input.GetKey(KeyCode.Space) && Time.time > nextShotTime)
        {
            nextShotTime = Time.time + fireRate;
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        // Calculate the angle offset for each bullet based on the number of bullets per shot
        float angleStep = 10f;  // 10-degree spread between bullets
        float startAngle = -(angleStep * (bulletsPerShot - 1)) / 2f; // Start at a negative offset to center the spread

        for (int i = 0; i < bulletsPerShot; i++)
        {
            // Calculate the angle for the current bullet
            float currentAngle = startAngle + (i * angleStep);

            // Apply the angle to the fire point's rotation, affecting the Z-axis for spread
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0f, 0f, currentAngle); // Apply to Z-axis for spread

            // Instantiate the projectile at the fire point's position with the calculated rotation
            Instantiate(projectilePrefab, firePoint.position, rotation);
        }

        // Play the shooting sound
        // AudioManager.Instance.PlaySFX("Shoot");
    }

    private void OnUpgradeSelected()
    {
        // Get the rank of MultiShot and AttackSpeed upgrades
        int attackSpeedRank = UpgradeManager.GetUpgradeRank("Attack Speed");
        int multiShotRank = UpgradeManager.GetUpgradeRank("MultiShot");

        // Adjust the fire rate based on the AttackSpeed upgrade
        if (attackSpeedRank > 0)
        {
            fireRate = Mathf.Max(0.1f, fireRate - (0.1f * attackSpeedRank)); // Lower fire rate with higher rank (e.g., faster shooting)
        }

        // Handle MultiShot logic (for example, firing multiple projectiles based on rank)
        if (multiShotRank > 0)
        {
            bulletsPerShot = multiShotRank + 1;
        }
    }
}
