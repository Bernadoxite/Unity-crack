using System.Collections;
using UnityEngine;

public class KetchupPunch : MonoBehaviour
{
    public GameObject visualPart;      // La partie du corps à cacher
    public GameObject clonePrefab;     // Le clone qui va bouger
    public float speed = 8f;

    public void Punch(bool faceRight)
    {
        StartCoroutine(PunchEffect(faceRight));
    }

    IEnumerator PunchEffect(bool faceRight)
    {
        // On cache la partie originale
        visualPart.GetComponent<SpriteRenderer>().enabled = false;

        //  On crée le clone au même endroit
        GameObject clone = Instantiate(clonePrefab, visualPart.transform.position, Quaternion.identity);

        float time = 0f;

        //  Aller
        while (time < 0.1f)
        {
            if (faceRight)
                clone.transform.Translate(Vector2.right * speed * Time.deltaTime);
            else
                clone.transform.Translate(Vector2.left * speed * Time.deltaTime);

            time += Time.deltaTime;
            yield return null;
        }

        // Retour
        time = 0f;
        while (time < 0.1f)
        {
            if (faceRight)
                clone.transform.Translate(Vector2.left * speed * Time.deltaTime);
            else
                clone.transform.Translate(Vector2.right * speed * Time.deltaTime);

            time += Time.deltaTime;
            yield return null;
        }

        //  On supprime le clone
        Destroy(clone);

        // 6On réaffiche la partie originale
        visualPart.GetComponent<SpriteRenderer>().enabled = true;
    }
}

