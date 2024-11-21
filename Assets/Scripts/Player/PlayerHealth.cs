using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public event Action<int> OnLivesChanged;
    public event Action OnPlayerDeath;

    private Invulnerability invulnerability;  // Reference to the Invulnerability script

    private void Start()
    {
        currentLives = maxLives;
        invulnerability = GetComponent<Invulnerability>();

        // Immediately notify the UI of the initial health value
        OnLivesChanged?.Invoke(currentLives);
    }

    public void TakeDamage()
    {
        if (invulnerability.IsInvulnerable) return;  // Ignore damage if invulnerable

        currentLives--;
        OnLivesChanged?.Invoke(currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            invulnerability.Activate();  // Trigger invulnerability
        }
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
        Destroy(gameObject);
    }
}
