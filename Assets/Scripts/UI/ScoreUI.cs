using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign in the Inspector

    private void OnEnable()
    {
        ScoreManager.OnScoreUpdated += UpdateScoreUI;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreUpdated -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {newScore}";
        }
    }
}
