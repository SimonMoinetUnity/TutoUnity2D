using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;

    public Rigidbody2D rb;
    public Animator animator;
    private Vector3 velocity = Vector3.zero;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;
    
    private bool isJumping; 
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public Transform GroundCheck;
    public float GroundCheckRadius;
    public LayerMask collisionLayer;

    private float HorizontalMovement;
    private float VerticalMovement;

    public static PlayerMovement  instance;

    private void Awake ()
    {
        if (instance!=null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scene");
            return;
        }

        instance = this;
    }

    void Update()
    {
        HorizontalMovement = Input.GetAxis("Horizontal")*moveSpeed*Time.fixedDeltaTime;
        VerticalMovement = Input.GetAxis("Vertical")*climbSpeed*Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed",characterVelocity);
        animator.SetBool("isClimbing",isClimbing);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, collisionLayer);
        MovePlayer(HorizontalMovement,VerticalMovement);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if(!isClimbing)
        {
            Vector3 TargetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, TargetVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f,jumpForce));
                isJumping = false;
            }
        }

        else
        {
            Vector3 TargetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, TargetVelocity, ref velocity, .05f);
        }
    }

    void Flip(float _velocity)
    {
        if(_velocity> 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if(_velocity< -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
    }
}
