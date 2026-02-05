using UnityEngine;

public class TorseKick : MonoBehaviour
{
    public float kickForce = 0.7f; // Ajuste selon ton torse
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(Random.Range(-kickForce, kickForce), ForceMode2D.Impulse);
    }
}

