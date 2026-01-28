using UnityEngine;

public class LegController : MonoBehaviour
{
    public Transform pelvis;
    public Transform leftLeg;
    public Transform rightLeg;

    [Header("Offsets")]
    public float legSpacing = 0.25f;
    public float legDownOffset = 0.6f;

    [Header("Walk")]
    public float stepHeight = 0.12f;
    public float walkSpeed = 8f;

    private float walkTime;
    private float input;

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        // Avancer le cycle seulement si on bouge
        if (Mathf.Abs(input) > 0.1f)
            walkTime += Time.deltaTime * walkSpeed;
        else
            walkTime = 0f;

        float sin = Mathf.Sin(walkTime);

        Vector3 basePos = pelvis.position + Vector3.down * legDownOffset;

        leftLeg.position = basePos +
            new Vector3(-legSpacing, Mathf.Max(0, sin) * stepHeight, 0);

        rightLeg.position = basePos +
            new Vector3(legSpacing, Mathf.Max(0, -sin) * stepHeight, 0);
    }
}

