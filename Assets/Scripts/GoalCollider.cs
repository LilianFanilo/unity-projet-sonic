using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    [SerializeField] private AudioClip GoalRingEndSoundClip;

    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {

            AudioManager.instance.PlayClip(GoalRingEndSoundClip);

            gameObject.SetActive(false);            

        }
    }
}
