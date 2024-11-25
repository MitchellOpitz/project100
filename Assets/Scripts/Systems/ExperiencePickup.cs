using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            Destroy(gameObject); // Destroy the experience orb
        }
    }
}
