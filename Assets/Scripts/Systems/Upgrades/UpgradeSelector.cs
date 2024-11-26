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

    private void Start()
    {
        SelectUpgrades();
        foreach(var upgrade in selectedUpgrades)
        {
            Debug.Log(upgrade);
        }
    }

    public void SelectUpgrades()
    {
        selectedUpgrades.Clear();

        while (selectedUpgrades.Count < optionsCount)
        {
            Upgrade randomUpgrade = allUpgrades[Random.Range(0, allUpgrades.Count)];

            /*
            if (randomUpgrade.rank >= randomUpgrade.maxRank || selectedUpgrades.Contains(randomUpgrade))
                continue;
            */

            selectedUpgrades.Add(randomUpgrade);
        }
    }

    public List<Upgrade> GetSelectedUpgrades()
    {
        return selectedUpgrades;
    }
}
