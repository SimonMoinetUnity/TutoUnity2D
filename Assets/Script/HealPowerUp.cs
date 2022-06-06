using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int healthPoints; 
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(pickupSound, transform.position);
            PlayerHealth.instance.healPlayer(healthPoints);
            Destroy(gameObject);
        }
    }
}
