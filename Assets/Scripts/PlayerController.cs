//using Unity.VisualScripting; // qu'est-ce que ça foutait là?
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    //public Transform otherPlayer; // pourquoi jme suis fait chier avec tout ça
    public float maxDistance = 10f;

    private Vector2 moveInput;
    private Rigidbody2D rb; // c'est le rigidbody qui bouge le joueur
    private bool isGrounded;

    private PlayerHealth health; // pour check si hitstun

    public GameManager gameManager; // pour check si round actif

    public PlayerInput playerInput;

    public bool isFacingRight = true;

    private float baseScaleX;






    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        playerInput = GetComponent<PlayerInput>();

        baseScaleX = transform.localScale.x;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        Debug.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.2f, isGrounded ? Color.green : Color.red);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (health != null && health.IsHit()) return; // bloque le mouvement horizontal si ko ou hitstun
        if (gameManager != null && !gameManager.isRoundActive) return; //bloque si round fini
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (gameManager != null && !gameManager.isRoundActive) return; 
        if (context.performed && isGrounded && (health == null || !health.IsHit()))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (moveInput.x > 0.01f)
        {
            isFacingRight = true;
        }
        else if (moveInput.x < -0.01f)
        {
            isFacingRight = false;
        }
        Vector3 scale = transform.localScale;
        scale.x = isFacingRight ? baseScaleX : -baseScaleX;
        transform.localScale = scale;



        if (health != null && health.IsHit())
        {
            rb.linearVelocity = new Vector2(0, rb.velocity.y);
            return;
        }

        if (gameManager != null && !gameManager.isRoundActive)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

            //float targetVelocityX = moveInput.x * speed;

            //if (otherPlayer != null)
            //{
            //    float distance = otherPlayer.position.x - transform.position.x;

            //    // On bloque la vitesse si on dépasse maxDistance
            //    if (distance > maxDistance && moveInput.x > 0) targetVelocityX = 0;
            //    if (distance < -maxDistance && moveInput.x < 0) targetVelocityX = 0;

            //    // On calcule la position future et on clamp si besoin
            //    float nextPosX = transform.position.x + targetVelocityX * Time.fixedDeltaTime;
            //    float minX = otherPlayer.position.x - maxDistance;
            //    float maxX = otherPlayer.position.x + maxDistance;
            //    nextPosX = Mathf.Clamp(nextPosX, minX, maxX);

            //    targetVelocityX = (nextPosX - transform.position.x) / Time.fixedDeltaTime;
            //}

            //rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);

            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }

}




