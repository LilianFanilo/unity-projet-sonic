using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private GameObject LoseMenu;
    public GoalCollider goalCollider;
    public GameRespawn gameRespawn;

    void FixedUpdate()
    {
        if (goalCollider != null)
        {
            if (goalCollider.Display_Win == true)
            {
                WinMenu.SetActive(true);
            } 
        }
    }
}
