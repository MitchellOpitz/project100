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
        // Instantiate the projectile at the fire point's position with its rotation
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // AudioManager.Instance.PlaySFX("Shoot");
    }
    private void OnUpgradeSelected()
    {
        // Get the rank of MultiShot and AttackSpeed upgrades
        int attackSpeedRank = UpgradeManager.GetUpgradeRank("Attack Speed");

        // Adjust the fire rate based on the AttackSpeed upgrade
        if (attackSpeedRank > 0)
        {
            fireRate = Mathf.Max(0.1f, fireRate - (0.1f * attackSpeedRank)); // Lower fire rate with higher rank (e.g., faster shooting)
            Debug.Log("New attack speed: " + fireRate);
        }
    }
}
