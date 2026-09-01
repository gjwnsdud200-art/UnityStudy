using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;
    private Rigidbody2D rb;
    
    public float jumpForce = 8f;
    public float speed = 10f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGround;
    private bool isSlide;

    void Move(float move)
    {
        //rb.AddForce(Vector2.right * move * speed);
        transform.position += Vector3.right * move * speed * Time.deltaTime;
    }

    void Flip(float move)
    {
        if (move > 0)
        {
            sr.flipX = false;
        }
        else if (move < 0)
        {
            sr.flipX = true;
        }
    }

   bool SlideInput()
    {
        return Input.GetKey(KeyCode.LeftControl) && isGround;
        
    }
    void UpdateAnimator(float move)
    {
        animator.SetBool("isGround", isGround);
        animator.SetBool("isRunning", move != 0);
        animator.SetBool("isSlide", isSlide);
    }

    bool CheckGround()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
            );
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && isGround)
        {
            animator.SetTrigger("Attack");
        }
    }

    /* -----------------------------------------------------------------------------------*/
    void Start()
    {
        Debug.Log("게임 시작!");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {


        isGround = CheckGround();
        isSlide = SlideInput();
        //Debug.Log(isGround);
        float move = Input.GetAxisRaw("Horizontal");
        Move(move);
        Flip(move);
        UpdateAnimator(move);
       

        //transform.position += Vector3.right * move * speed * Time.deltaTime;

        //rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
        Jump();
        Attack();
    }
}
