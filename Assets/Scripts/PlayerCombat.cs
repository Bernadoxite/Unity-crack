using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public int damage = 10; // public pour changer selon perso.
    public Transform attackPoint; // a mettre sur enfant du GO !!
    public float attackRange = 0.5f;
    //public LayerMask enemyLayer;
    public LayerMask playerLayer;

    private PlayerHealth health; // pour savoir si on est hitstun

    public GameManager gameManager; // savoir si round fini

  


    void Awake()
    {
        health = GetComponent<PlayerHealth>();
     

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (gameManager != null && !gameManager.isRoundActive) return;//bloque attaque si round fini

        // Bloque l'attaque si le joueur est stun
        if (health != null && health.IsHit()) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer); // détecte ennemis dans la zone

        foreach (Collider2D hit in hits) //(hit != null && hit.gameObject != gameObject) // le collider rencontré s'appelle hit et n'est pas sur ce gameobject
        {
            if (hit.gameObject == gameObject)
                continue;
            
            if (hit.TryGetComponent(out PlayerHealth ph))
            {
             ph.TakeDamage(damage);
             Debug.Log(gameObject.name + " touche " + hit.name);// on appelle le TD du playerhealth du GO de ce collider.
                break;
            }
            //hit.GetComponent<PlayerHealth>()?.TakeDamage(damage); 
            
            
        

        }
    }
  

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}




