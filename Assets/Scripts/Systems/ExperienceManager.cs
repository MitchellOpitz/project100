using UnityEngine;
using System;

public class ExperienceManager : MonoBehaviour
{
    public static event Action<int, int> OnExperienceUpdated; // Event for UI updates: currentExperience, experienceNeededForNextLevel

    private int currentExperience = 0;
    private int currentLevel = 1;
    private int experienceNeededForNextLevel;

    [SerializeField] private int baseExperienceForLevel = 100; // Base experience required for level 1
    [SerializeField] private float experienceGrowthFactor = 1.5f; // Multiplier for each level's experience requirement
    [SerializeField] private int experiencePerPickup = 10; // Experience gained per pickup

    private void Start()
    {
        // Initialize experience needed for the first level
        experienceNeededForNextLevel = CalculateExperienceForLevel(currentLevel);
    }

    private void OnEnable()
    {
        ExperiencePickup.OnExperiencePickedUp += OnExperiencePickedUp;
    }

    private void OnDisable()
    {
        ExperiencePickup.OnExperiencePickedUp -= OnExperiencePickedUp;
    }

    private void OnExperiencePickedUp(ExperiencePickup pickup)
    {
        currentExperience += experiencePerPickup;
        Debug.Log($"Experience picked up! Current Experience: {currentExperience}/{experienceNeededForNextLevel}");

        // Trigger the UI update event
        OnExperienceUpdated?.Invoke(currentExperience, experienceNeededForNextLevel);

        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        while (currentExperience >= experienceNeededForNextLevel)
        {
            currentExperience -= experienceNeededForNextLevel;
            currentLevel++;

            // Update the experience needed for the next level
            experienceNeededForNextLevel = CalculateExperienceForLevel(currentLevel);
            OnExperienceUpdated?.Invoke(currentExperience, experienceNeededForNextLevel);

            Debug.Log($"Level Up! Current Level: {currentLevel}");
        }
    }

    private int CalculateExperienceForLevel(int level)
    {
        return Mathf.RoundToInt(baseExperienceForLevel * Mathf.Pow(experienceGrowthFactor, level - 1));
    }
}
