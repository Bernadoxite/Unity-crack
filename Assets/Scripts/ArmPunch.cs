using UnityEngine;

public class ArmPunch : MonoBehaviour
{
   
    public Rigidbody2D armRigidbody;
    public float force = 5f;

    public void Punch(bool faceRight)
    {
        if (armRigidbody == null) return;

        Vector2 direction;

        if (faceRight)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        armRigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }


}
