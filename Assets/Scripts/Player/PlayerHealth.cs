using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public event Action<int> OnLivesChanged;  // Event to notify UI of lives change
    public event Action OnPlayerDeath;        // Event for game over

    public float invulnerabilityDuration = 3f;  // Duration of invulnerability after taking damage
    private bool isInvulnerable = false;        // Tracks if the player is invulnerable
    private SpriteRenderer spriteRenderer;      // Reference for flashing effect during invulnerability

    private void Start()
    {
        currentLives = maxLives;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        if (isInvulnerable) return;  // Ignore damage if invulnerable

        currentLives--;
        OnLivesChanged?.Invoke(currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(HandleInvulnerability());
        }
    }

    private System.Collections.IEnumerator HandleInvulnerability()
    {
        isInvulnerable = true;

        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;  // Toggle visibility for flashing
            elapsedTime += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }

        spriteRenderer.enabled = true;  // Ensure visibility is restored
        isInvulnerable = false;
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
        Destroy(gameObject);
    }
}
