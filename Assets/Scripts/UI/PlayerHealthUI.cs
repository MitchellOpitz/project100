using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;  // Reference to the TextMeshProUGUI component

    private void Start()
    {
        // Find the PlayerHealth component and subscribe to the OnLivesChanged event
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged += UpdateHealthDisplay;
            UpdateHealthDisplay(playerHealth.currentLives);  // Initialize the health display
        }
    }

    // This method is called when the player's health changes
    private void UpdateHealthDisplay(int currentLives)
    {
        healthText.text = "Lives: " + currentLives;  // Update the TextMeshProUGUI with the current lives
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks if this object is destroyed
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged -= UpdateHealthDisplay;
        }
    }
}
