using UnityEngine;
using System.Collections.Generic;

public class UpgradeSelector : MonoBehaviour
{
    [Header("All Available Upgrades")]
    public List<Upgrade> allUpgrades;

    [Header("Currently Selected Upgrades")]
    public List<Upgrade> selectedUpgrades = new List<Upgrade>();

    [Header("Number of Options")]
    public int optionsCount = 3;

    [Header("Max Attempts to Find Upgrades")]
    public int maxAttempts = 25;

    public void SelectUpgrades()
    {
        selectedUpgrades.Clear();
        int attempts = 0;

        while (selectedUpgrades.Count < optionsCount && attempts < maxAttempts)
        {
            attempts++;

            Upgrade randomUpgrade = allUpgrades[Random.Range(0, allUpgrades.Count)];

            // Skip upgrades that are at max rank or already selected
            if (UpgradeManager.IsUpgradeMaxRank(randomUpgrade.upgradeName) || selectedUpgrades.Contains(randomUpgrade))
                continue;

            selectedUpgrades.Add(randomUpgrade);
        }

        if (selectedUpgrades.Count < optionsCount)
        {
            Debug.LogWarning($"Could not find enough upgrades. Only selected {selectedUpgrades.Count} upgrades after {attempts} attempts.");
        }
    }

    public List<Upgrade> GetSelectedUpgrades()
    {
        SelectUpgrades();
        return selectedUpgrades;
    }
}
