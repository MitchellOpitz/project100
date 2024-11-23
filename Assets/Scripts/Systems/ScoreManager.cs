using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public static event Action<int> OnScoreUpdated;

    private int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += AddScore;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= AddScore;
    }

    private void AddScore(int value)
    {
        Debug.Log("Score added: " + value);
        score += value;
        OnScoreUpdated?.Invoke(score); // Notify listeners of the updated score
    }

    public int GetScore()
    {
        return score;
    }
}
