using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            if (playerController.isHomingAttackActive == true)
            {
                gameObject.SetActive(false);
            } 
            else
            {
                ScoreManager.Instance.RemoveScore();
            }

        }
    }
}
