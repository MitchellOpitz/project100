using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static event Action OnUpgradeSelected;

    // This method will be called from the UI to apply the selected upgrade
    public static void ApplyUpgrade(Upgrade selectedUpgrade)
    {
        // Example logic for applying the upgrade (for now, just log the selection)
        Debug.Log($"Selected {selectedUpgrade.upgradeName} upgrade!");

        // Trigger the event to notify other systems (e.g., UI to hide the menu, game logic to update the state)
        OnUpgradeSelected?.Invoke();
    }
}
