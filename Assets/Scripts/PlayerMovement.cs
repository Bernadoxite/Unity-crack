//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerMovement : MonoBehaviour
//{
//    public Rigidbody2D torsoRb;

//    [Header("Movement")]
//    public float moveForce = 60f;
//    public float maxSpeed = 5f;

//    private float input;

//    void Update()
//    {
//        input = Input.GetAxisRaw("Horizontal");
//    }

//    void FixedUpdate()
//    {
//        // Appliquer une force horizontale
//        torsoRb.AddForce(Vector2.right * input * moveForce);

//        // Limiter la vitesse max
//        Vector2 vel = torsoRb.linearVelocity;
//        vel.x = Mathf.Clamp(vel.x, -maxSpeed, maxSpeed);
//        torsoRb.linearVelocity = vel;
//    }


//}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    float input;

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);
    }
}


