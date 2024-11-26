using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText; // Reference to the TextMeshPro component

    private void OnEnable()
    {
        ExperienceManager.OnLevelUp += UpdateWaveUI;
    }

    private void OnDisable()
    {
        ExperienceManager.OnLevelUp -= UpdateWaveUI;
    }

    private void UpdateWaveUI(int currentLevel)
    {
        waveText.text = $"Wave {currentLevel}";
    }
}
