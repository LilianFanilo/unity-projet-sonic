using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float timeCounter;
    [SerializeField] private float countdownTimer = 120f;
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    [SerializeField] private bool isCountdown;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Update()
    {
        if (isCountdown && countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            minutes = Mathf.FloorToInt(countdownTimer / 60f);
            seconds = Mathf.FloorToInt(countdownTimer - minutes * 60);            
        } else if (!isCountdown) 
        {
            timeCounter += Time.deltaTime;
            minutes = Mathf.FloorToInt(timeCounter / 60f);
            seconds = Mathf.FloorToInt(timeCounter - minutes * 60);  
        }
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
