using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For handling UI elements like buttons.
using TMPro; // For TextMesh Pro components

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenuUI;
    [SerializeField] private float waitTimeBeforeUI;
    [SerializeField] private UpgradeSelector upgradeSelector;
    [SerializeField] private Transform upgradeOptionsParent; // Container to hold the upgrade buttons and details
    [SerializeField] private GameObject upgradeOptionPrefab; // Prefab for each upgrade option

    // Start is called before the first frame update
    private void OnEnable()
    {
        ExperienceManager.OnLevelUp += HandleLevelUp;
        UpgradeManager.OnUpgradeSelected += CloseUpgradeMenuUI;
    }

    private void OnDisable()
    {
        ExperienceManager.OnLevelUp -= HandleLevelUp;
        UpgradeManager.OnUpgradeSelected -= CloseUpgradeMenuUI;
    }

    private void HandleLevelUp(int level)
    {
        StartCoroutine(ShowUpgradeMenu());
    }

    private IEnumerator ShowUpgradeMenu()
    {
        yield return new WaitForSeconds(waitTimeBeforeUI);

        // Get the selected upgrades from the UpgradeSelector
        List<Upgrade> selectedUpgrades = upgradeSelector.GetSelectedUpgrades();

        // Clear existing options before populating new ones
        foreach (Transform child in upgradeOptionsParent)
        {
            Destroy(child.gameObject);
        }

        // Populate the UI with the selected upgrades
        foreach (Upgrade upgrade in selectedUpgrades)
        {
            // Instantiate a new upgrade option UI item from prefab
            GameObject upgradeOption = Instantiate(upgradeOptionPrefab, upgradeOptionsParent);

            // Set the title, icon, and description using TextMeshProUGUI
            TMP_Text titleText = upgradeOption.transform.Find("TitleText").GetComponent<TMP_Text>();
            Image iconImage = upgradeOption.transform.Find("IconImage").GetComponent<Image>();
            TMP_Text descriptionText = upgradeOption.transform.Find("DescriptionText").GetComponent<TMP_Text>();
            Button selectButton = upgradeOption.transform.Find("SelectButton").GetComponent<Button>();

            titleText.text = upgrade.upgradeName;
            iconImage.sprite = upgrade.icon;
            descriptionText.text = upgrade.description;

            // Set the button onClick listener to select this upgrade
            selectButton.onClick.AddListener(() => SelectUpgrade(upgrade));
        }

        // Show the upgrade menu
        upgradeMenuUI.SetActive(true);
    }

    private void CloseUpgradeMenuUI()
    {
        upgradeMenuUI.SetActive(false);
    }
    
    private void SelectUpgrade(Upgrade selectedUpgrade)
    {
        // Notify the UpgradeManager (or other relevant systems) about the selected upgrade
        UpgradeManager.ApplyUpgrade(selectedUpgrade);

        // Close the upgrade menu after selection
        CloseUpgradeMenuUI();
    }
}
