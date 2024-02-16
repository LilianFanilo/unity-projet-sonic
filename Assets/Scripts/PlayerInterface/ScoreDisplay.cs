using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI RingsText;

    private void Awake()
    {
        RingsText = GetComponent<TextMeshProUGUI>();
        ScoreManager.Instance.OnAddScore += UpdateScore;
    }
    private void UpdateScore()
    {
        RingsText.text = "Rings : " + ScoreManager.Instance.Score;
    }
}
