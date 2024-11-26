using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static event Action OnUpgradeSelected;
    [SerializeField] private Button upgradeButton;

    // Start is called before the first frame update
    void Start()
    {
        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(HandleUpgradeSelection);
        }
    }

    private void HandleUpgradeSelection()
    {
        OnUpgradeSelected?.Invoke();
    }
}
