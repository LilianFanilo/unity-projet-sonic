using UnityEngine;

public class RotationContinuScript : MonoBehaviour
{
    // Vitesse de rotation en degr�s par seconde
    public float vitesseRotation = 30f;

    void Update()
    {
        // Calcul de la rotation en fonction de la vitesse et du temps
        float rotation = vitesseRotation * Time.deltaTime;

        // Appliquer la rotation � l'objet autour de son axe Y
        transform.Rotate(Vector3.up, rotation);
    }
}

