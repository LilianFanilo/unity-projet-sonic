using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager
{
    #region Singleton
    private static ScoreManager m_Instance;

    public static ScoreManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new ScoreManager();
            }

            return m_Instance;
        }
    }
    #endregion

    public static readonly int MAXMIMUM_SCORE = 4;

    public event Action OnAddScore;

    public int Score { get; private set; }
    public void AddScore()
    {
        Score++;
        if (OnAddScore != null)
        {    
            OnAddScore();
        }
    }
}
