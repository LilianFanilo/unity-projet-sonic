using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    PlayerController playerController;

    private const string PLAYER_TAG = "Player";

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            ScoreManager.Instance.RemoveScore();

            gameObject.SetActive(false);
        }
    }
}
