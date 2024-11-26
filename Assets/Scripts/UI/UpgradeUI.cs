using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenuUI;
    [SerializeField] private float waitTimeBeforeUI;

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
        upgradeMenuUI.SetActive(true);
    }

    private void CloseUpgradeMenuUI()
    {
        upgradeMenuUI.SetActive(false);
    }
}
