using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static event Action OnUpgradeSelected;

    private static Dictionary<string, Upgrade> activeUpgrades = new Dictionary<string, Upgrade>();

    // This method will be called from the UI to apply the selected upgrade
    public static void ApplyUpgrade(Upgrade selectedUpgrade)
    {
        // Check if the upgrade is already in the dictionary
        if (activeUpgrades.ContainsKey(selectedUpgrade.upgradeName))
        {
            // Increment the rank if already in the list
            activeUpgrades[selectedUpgrade.upgradeName].rank++;
        }
        else
        {
            // Otherwise, add the new upgrade with a rank of 1
            selectedUpgrade.rank = 1;
            activeUpgrades.Add(selectedUpgrade.upgradeName, selectedUpgrade);
        }

        // Example logic for applying the upgrade (for now, just log the selection)
        Debug.Log($"Selected {selectedUpgrade.upgradeName} upgrade with rank {selectedUpgrade.rank}!");

        // Trigger the event to notify other systems (e.g., UI to hide the menu, game logic to update the state)
        OnUpgradeSelected?.Invoke();
    }

    // Method to get the rank of an upgrade by its name
    public static int GetUpgradeRank(string upgradeName)
    {
        if (activeUpgrades.ContainsKey(upgradeName))
        {
            return activeUpgrades[upgradeName].rank;
        }
        else
        {
            return 0; // Return 0 if the upgrade is not in the list
        }
    }
}