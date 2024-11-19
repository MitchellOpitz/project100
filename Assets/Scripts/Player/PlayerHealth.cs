using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public event Action<int> OnLivesChanged;  // Event to notify UI of lives change
    public event Action OnPlayerDeath;        // Event for game over

    void Start()
    {
        currentLives = maxLives;
    }

    // Method to handle taking damage
    public void TakeDamage()
    {
        currentLives--;
        OnLivesChanged?.Invoke(currentLives);  // Notify any listeners (UI updates)

        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();  // Trigger game over event
    }
}
