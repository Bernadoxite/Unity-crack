using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothSpeed = 5f; // vitesse d'interpolation, plus c grand plus la cam est réactive

    private void LateUpdate() // LUD car suit la position finale par frame des players
    {
        if (player1 == null || player2 == null) return; // besoin de cette sécu?

        // Point médian entre les deux joueurs
        float midX = (player1.position.x + player2.position.x) / 2f;
        float Y = 2; //  pas de mid vertical ici?? Sauf si mega jump ??

        Vector3 targetPosition = new Vector3(midX, Y, transform.position.z); // pareil ici midy utile?

        // Déplacement fluide
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}



