using UnityEngine;

public class VisualLegs : MonoBehaviour
{
    //public Transform leftLeg;
    //public Transform rightLeg;

    //public float stepSpeed = 8f;
    //public float stepAmplitude = 25f;

    //float walkTime;

    //void Update()
    //{
    //    float speed = Mathf.Abs(Input.GetAxisRaw("Horizontal"));

    //    if (speed > 0.1f)
    //    {
    //        walkTime += Time.deltaTime * stepSpeed;
    //        float angle = Mathf.Sin(walkTime) * stepAmplitude;

    //        leftLeg.localRotation = Quaternion.Euler(0, 0, angle);
    //        rightLeg.localRotation = Quaternion.Euler(0, 0, -angle);
    //    }
    //    else
    //    {
    //        leftLeg.localRotation = Quaternion.identity;
    //        rightLeg.localRotation = Quaternion.identity;
    //    }
    //}

    




    [Header("Leg References")]
    public Transform leftLeg;
    public Transform rightLeg;

    [Header("Step Timing")]
    public float stepSpeed = 8f;

    [Header("Vertical Motion")]
    public float stepHeight = 0.04f;

    [Header("Horizontal Spread")]
    public float stepSpread = 0.03f;

    [Header("Swing Rotation")]
    public float swingAngle = 8f;

    [Header("Damping")]
    public float returnSpeed = 10f;

    [Header("Phase")]
    [Tooltip("Décalage de phase entre les jambes (PI = marche classique)")]
    public float phaseOffset = Mathf.PI;

    Rigidbody2D rb;


    Vector2 leftRestPos;
    Vector2 rightRestPos;
    Quaternion leftRestRot;
    Quaternion rightRestRot;

    float time;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>(); 
        
        leftRestPos = leftLeg.localPosition;
        rightRestPos = rightLeg.localPosition;

        leftRestRot = leftLeg.localRotation;
        rightRestRot = rightLeg.localRotation;
    }

    void Update()
    {
        //float input = Mathf.Abs(Input.GetAxisRaw("Horizontal"));

        float speed = Mathf.Abs(rb.velocity.x);


        if (speed > 0.1f)
        {
            time += Time.deltaTime * stepSpeed;

            // Phase des jambes
            float phaseL = Mathf.Sin(time);
            float phaseR = Mathf.Sin(time + phaseOffset);

            float liftL = Mathf.Abs(Mathf.Cos(time)) * stepHeight;
            float liftR = Mathf.Abs(Mathf.Cos(time + phaseOffset)) * stepHeight;

            float spreadL = phaseL * stepSpread;
            float spreadR = phaseR * stepSpread;

            float angleL = phaseL * swingAngle;
            float angleR = phaseR * swingAngle;

            leftLeg.localPosition = leftRestPos + new Vector2(-spreadL, liftL);
            rightLeg.localPosition = rightRestPos + new Vector2(spreadR, liftR);

            leftLeg.localRotation = Quaternion.Euler(0, 0, angleL);
            rightLeg.localRotation = Quaternion.Euler(0, 0, -angleR);
        }
        else
        {
            leftLeg.localPosition = Vector2.Lerp(
                leftLeg.localPosition, leftRestPos, Time.deltaTime * returnSpeed);

            rightLeg.localPosition = Vector2.Lerp(
                rightLeg.localPosition, rightRestPos, Time.deltaTime * returnSpeed);

            leftLeg.localRotation = Quaternion.Lerp(
                leftLeg.localRotation, leftRestRot, Time.deltaTime * returnSpeed);

            rightLeg.localRotation = Quaternion.Lerp(
                rightLeg.localRotation, rightRestRot, Time.deltaTime * returnSpeed);
        }
    }
}











