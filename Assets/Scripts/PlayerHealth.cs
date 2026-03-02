using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth; //modifiable uniquement par le script

    public float hitStunDuration = 0.2f;
    private bool isHit = false;

    private HealthBar healthBar;

    public System.Action<PlayerHealth> OnKO; // event qui prévient quand le joueur est KO

    private SpriteRenderer[] spriteRenderers;
    public Color hurtColor = Color.red;
    

    void Awake()
    {
        
        healthBar = GetComponentInChildren<HealthBar>(); // Récupère la health bar dans les enfants du prefab

        currentHealth = maxHealth; //initialise la vie

        if (healthBar != null) 
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        spriteRenderers= GetComponentsInChildren<SpriteRenderer>();
    }


    
    public void TakeDamage(int damage)
    {
        if (isHit) return; // évite le spam de dégâts pendant le stun

        currentHealth -= damage; // enleve le nombre de PV défini dans playercombat
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 

        Debug.Log(gameObject.name + " : " + currentHealth + " PV");

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Debug.Log(gameObject.name + " EST KO !");
            
            OnKO?.Invoke(this);

            return;
            

        }

        StartCoroutine(HitStun()); 
    }


    private IEnumerator HitStun()
    {
        isHit = true;
        foreach (SpriteRenderer sprite in spriteRenderers)
        {
            
            sprite.color = hurtColor; // Change to red
            yield return new WaitForSeconds(hitStunDuration);
            
        }
            
        
        isHit = false;
    }

    public bool IsHit()
    {
        return isHit; // pour dire aux autres scripts si le player est en hitstun sans rendre la bool publique (Fonctions Isquelque chose qui font un truc si la réponse est "oui")
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isHit = false;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

}


