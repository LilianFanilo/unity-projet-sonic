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
            gameObject.SetActive(false);

            // Je voulais faire en sorte que le robot se d�truise quand il est touch� par une attaque
            // ou qu'il fasse perdre des rings sinon mais je ne sais pas pourquoi mes bool�ans retournent toujours un r�sultat false
        }
    }
}
