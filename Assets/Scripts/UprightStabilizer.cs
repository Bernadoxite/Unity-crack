using UnityEngine;

public class UprightStabilizer : MonoBehaviour
{
    public float uprightStrength = 50f;
    public float damping = 8f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float angle = Mathf.DeltaAngle(rb.rotation, 0f);
        float torque = -angle * uprightStrength - rb.angularVelocity * damping;
        rb.AddTorque(torque);
    }
}
