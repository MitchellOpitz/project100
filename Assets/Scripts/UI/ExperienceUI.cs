using UnityEngine;
using TMPro;

public class ExperienceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text experienceText; // Reference to the TextMeshPro component

    private void OnEnable()
    {
        ExperienceManager.OnExperienceUpdated += UpdateExperienceUI;
    }

    private void OnDisable()
    {
        ExperienceManager.OnExperienceUpdated -= UpdateExperienceUI;
    }

    private void UpdateExperienceUI(int currentExp, int nextLevelExp)
    {
        experienceText.text = $"EXP: {currentExp}/{nextLevelExp}";
    }
}
