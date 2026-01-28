using UnityEngine;

public class Ragedidoll : MonoBehaviour
{
  

    public Rigidbody2D torso;
    public float moveForce = 50f;
    public float maxSpeed = 6f;

    void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(torso.velocity.x) < maxSpeed)
        {
            torso.AddForce(Vector2.right * input * moveForce);
        }
    }
}

