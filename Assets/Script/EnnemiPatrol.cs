using UnityEngine;

public class EnnemiPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] wayPoints;
    private Transform Target;
    private int destPoint = 0;
    public SpriteRenderer graphics;
    public int damageOnCollision = 20;

    void Start()
    {
        Target = wayPoints[0];
    }

    void Update()
    {
        Vector3 dir = Target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World);
       
        if (Vector3.Distance (transform.position, Target.position)<0.3f)
        {
            destPoint = (destPoint + 1) % wayPoints.Length;
            Target = wayPoints[destPoint];
            graphics.flipX =! graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.takeDomage(damageOnCollision);
        }
    }
}
