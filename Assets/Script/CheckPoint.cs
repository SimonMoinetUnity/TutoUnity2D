using UnityEngine;

public class CheckPoint : MonoBehaviour
{   
    void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentSceneManager.instance.respawnPoint = transform.position;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
