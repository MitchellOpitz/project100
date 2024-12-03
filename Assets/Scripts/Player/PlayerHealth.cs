using System;
using UnityEngine;
using FMODUnity;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public event Action<int> OnLivesChanged;
    public event Action OnPlayerDeath;

    private int healthRegenRank;

    private Invulnerability invulnerability;  // Reference to the Invulnerability script

    private void Start()
    {
        healthRegenRank = 0;
        currentLives = maxLives;
        invulnerability = GetComponent<Invulnerability>();

        // Immediately notify the UI of the initial health value
        OnLivesChanged?.Invoke(currentLives);
    }

    private void OnEnable()
    {
        UpgradeManager.OnUpgradeSelected += OnUpgradeSelected;
        WaveManager.OnWaveChanged += _ => OnWaveStart();
    }

    private void OnDisable()
    {
        UpgradeManager.OnUpgradeSelected -= OnUpgradeSelected;
        WaveManager.OnWaveChanged -= _ => OnWaveStart();
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
            RuntimeManager.PlayOneShot("event:/PlayerHurt");
            invulnerability.Activate();  // Trigger invulnerability
        }
    }

    private void Die()
    {
        RuntimeManager.PlayOneShot("event:/PlayerDeath");
        OnPlayerDeath?.Invoke();
        Destroy(gameObject);
    }

    private void OnUpgradeSelected()
    {
        maxLives = UpgradeManager.GetUpgradeRank("Max Health") + 3;
        healthRegenRank = UpgradeManager.GetUpgradeRank("Health Regen");
    }

    private void OnWaveStart()
    {
        currentLives = currentLives + healthRegenRank;
        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }
        OnLivesChanged?.Invoke(currentLives);
    }
}
