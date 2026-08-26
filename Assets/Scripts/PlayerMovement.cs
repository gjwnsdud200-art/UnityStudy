using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;
    public float speed = 0.1f;
    private Rigidbody2D rb;
    
    public float jumpForce = 8f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGround;

    void Start()
    {
        Debug.Log("게임 시작!");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
            );
        animator.SetBool("isGround", isGround);

        float move = Input.GetAxisRaw("Horizontal");
        if (move > 0)
        {
            sr.flipX = false;
        }
        else if (move < 0)
        {
            sr.flipX = true;
        }

        animator.SetBool("isRunning", move != 0);

        //transform.position += Vector3.right * move * speed * Time.deltaTime;

        //rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        rb.AddForce(Vector2.right * move * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGround) 
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.J) && isGround)
        {
            animator.SetTrigger("Attack");
        }
    }
}
