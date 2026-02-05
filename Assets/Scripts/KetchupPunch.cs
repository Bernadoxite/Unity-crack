using UnityEngine;
using System.Collections;

public class KetchupPunch : MonoBehaviour
{
    
    public Rigidbody2D layerRb;
    public Joint2D jointTop;
    public Joint2D jointBottom;

    public float moveDistance = 0.3f;
    public float moveTime = 0.08f;

    private Vector2 startPosition;
    public void Punch(bool faceRight)
    {
        StartCoroutine(PunchRoutine(faceRight));
    }

    IEnumerator PunchRoutine(bool faceRight)
    {
        Vector2 dir = faceRight ? Vector2.right : Vector2.left;

        //   mémorise la position
        Vector2 startPos = layerRb.position;

        //  libère la tranche
        jointTop.enabled = false;
        jointBottom.enabled = false;

        //  avance
        layerRb.MovePosition(startPos + dir * moveDistance);

        //  pause
        yield return new WaitForSeconds(0.08f);

        //  revient
        layerRb.MovePosition(startPos);

        //  rattache
        jointTop.enabled = true;
        jointBottom.enabled = true;
    }

}
