using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;  // Delay between shots when holding down fire button

    private float nextShotTime = 0f;

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
}
