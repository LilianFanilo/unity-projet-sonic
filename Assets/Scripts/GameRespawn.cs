using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public float threshold;
    public int playerLives = 3;

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(-0.01f, 10.22f, 0.05f);
            playerLives -= 1;
            Debug.Log(playerLives);
        }
    }
}