using UnityEngine;

public class Walkanim : MonoBehaviour
{
    
   

    [Header("Rigidbody du Torso")]
    public Rigidbody2D torso;

    [Header("Jambes pour animation visuelle")]
    public Transform upperLegL;
    public Transform upperLegR;
    public float legSwingAngle = 20f; // amplitude du balancement
    public float swingSpeed = 4f;     // vitesse du balancement

    [Header("Déplacement")]
    public float moveSpeed = 5f;

    private float timer = 0f;

    void FixedUpdate()
    {
        // Déplacement horizontal du Torso
        float input = Input.GetAxis("Horizontal"); // -1 à 1
        torso.velocity = new Vector2(input * moveSpeed, torso.velocity.y);

        // Animation simple des jambes
        timer += Time.fixedDeltaTime * swingSpeed;
        float swing = Mathf.Sin(timer) * legSwingAngle;

        // Jambes gauche/droite en opposition
        if (upperLegL != null) upperLegL.localRotation = Quaternion.Euler(0, 0, swing);
        if (upperLegR != null) upperLegR.localRotation = Quaternion.Euler(0, 0, -swing);
    }






}
