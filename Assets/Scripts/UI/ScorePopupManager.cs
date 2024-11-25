using UnityEngine;
using TMPro;

public class ScorePopupManager : MonoBehaviour
{
    [SerializeField] private GameObject scorePopupPrefab;  // The prefab to instantiate
    [SerializeField] private Canvas canvas;                // The canvas to instantiate on

    private void OnEnable()
    {
        // Subscribe to the OnEnemyKilled event
        Enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnEnemyKilled event
        Enemy.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void HandleEnemyKilled(int score, Vector3 enemyPosition)
    {
        if (scorePopupPrefab == null || canvas == null) return;

        // Add 1 unit above the enemy's position (adjusting the Y-axis)
        Vector3 positionAboveEnemy = enemyPosition + Vector3.up;  // Adds 1 unit along the Y-axis

        // Instantiate the popup at the new position
        GameObject popup = Instantiate(scorePopupPrefab, positionAboveEnemy, Quaternion.identity);

        // Set the popup as a child of the canvas
        popup.transform.SetParent(canvas.transform, false);

        // Initialize the popup with the score
        ScorePopup popupScript = popup.GetComponent<ScorePopup>();
        popupScript.Initialize(score);
    }

}
