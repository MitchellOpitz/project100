using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private int totalExperience = 0;
    [SerializeField] private int experiencePerPickup = 10; // Placeholder experience amount

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
        totalExperience += experiencePerPickup;
        Debug.Log($"Experience picked up! Total Experience: {totalExperience}");
    }
}
