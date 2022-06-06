using UnityEngine;
using System.Collections; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isInvicible = false;

    public float InvicibilityFlashDelay = 0.2f;
    public float InvicibilityTime = 3f;
    public HealthBar healthBar;
    public SpriteRenderer graphics;

    public AudioClip hitSound;

    public static PlayerHealth  instance;

    private void Awake ()
    {
        if (instance!=null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scene");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            takeDomage(50);
        }
    }

    public void takeDomage(int damage)
    {
        if (!isInvicible)
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvicible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
    }

    public void Die()
    {
        PlayerMovement.instance.enabled = false; 
        PlayerMovement.instance.animator.SetTrigger("Die"); 
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic; 
        PlayerMovement.instance.rb.velocity = Vector3.zero; 
        PlayerMovement.instance.playerCollider.enabled = false; 
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true; 
        PlayerMovement.instance.animator.SetTrigger("Respawn"); 
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic; 
        PlayerMovement.instance.playerCollider.enabled = true; 
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void healPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        healthBar.SetHealth(currentHealth);
        
    }

    public IEnumerator InvicibilityFlash()
    {
        while(isInvicible)
        {
            graphics.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(InvicibilityFlashDelay);
            graphics.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(InvicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(InvicibilityTime);
        isInvicible = false;
    }
    
}
