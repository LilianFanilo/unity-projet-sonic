using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI ScoreText;

    private void Awake()
    {
        ScoreText = GetComponent<TextMeshProUGUI>();
        ScoreManager.Instance.OnAddScore += UpdateScore;
    }
    private void UpdateScore()
    {
        ScoreText.text = "Rings : " + ScoreManager.Instance.Score;
    }
}
