using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Countdown : MonoBehaviour
{

    public static event Action OnCountdownComplete;

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float countdownStartTime = 3f;
    [SerializeField] private float countdownInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private void OnEnable()
    {
        UpgradeManager.OnUpgradeSelected += StartCountdown;
    }

    private void OnDisable()
    {
        UpgradeManager.OnUpgradeSelected -= StartCountdown;
    }

    private void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true);
        float currentTime = countdownStartTime;
        yield return null;

        while (currentTime > 0)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString();
            currentTime -= countdownInterval;
            yield return new WaitForSeconds(countdownInterval);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(countdownInterval);
        OnCountdownComplete?.Invoke();
        countdownText.gameObject.SetActive(false);
    }
}
