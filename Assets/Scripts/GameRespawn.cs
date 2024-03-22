using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameRespawn : MonoBehaviour
{
    public float threshold;
    [SerializeField] private AudioClip DeathSoundClip;

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            AudioManager.instance.PlayClip(DeathSoundClip);
            transform.position = new Vector3(-0.01f, 10.22f, 0.05f);
        }
    }
}