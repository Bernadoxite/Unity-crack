using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class TorseMotorOscillation : MonoBehaviour
{
    public float springStrength = 12f;
    public float damping = 11f;
    public float maxMotorTorque = 20f;

    private HingeJoint2D hj;

    void Start()
    {
        hj = GetComponent<HingeJoint2D>();
    }

    void FixedUpdate()
    {
        float angle = hj.jointAngle;
        float motorSpeed = -angle * springStrength - hj.jointSpeed * damping;

        JointMotor2D motor = hj.motor;
        motor.motorSpeed = motorSpeed;
        motor.maxMotorTorque = maxMotorTorque;
        hj.motor = motor;
    }
}


