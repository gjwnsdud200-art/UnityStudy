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

    public Transform attackPoint; // 공격용 변수들
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayer;  // 적만 공격 대상으로 하기위한 레이어.
    private Vector3 attackPointStartPosition; 




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

            attackPoint.localPosition = // 오른쪽을 볼 때 AttackPoint도 오른쪽
                new Vector3(
                    Mathf.Abs(attackPointStartPosition.x),
                    attackPointStartPosition.y,
                    attackPointStartPosition.z
                    );
        }
        else if (move < 0)
        {
            sr.flipX = true;

            attackPoint.localPosition = // 왼쪽을 볼 때 AttackPoint도 왼쪽
                new Vector3(
                    -Mathf.Abs(attackPointStartPosition.x),
                    attackPointStartPosition.y,
                    attackPointStartPosition.z
                    );
        }
    }
/*
   bool SlideInput()
    {
        return Input.GetKey(KeyCode.LeftControl) && isGround;
        
    } */
    void UpdateAnimator(float move)
    {
        animator.SetBool("isGround", isGround);
        animator.SetBool("isRunning", move != 0);
        //animator.SetBool("isSlide", isSlide);

        animator.SetFloat("VerticalSpeed", rb.linearVelocity.y);
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

            Collider2D[] hitEnemies = // 배열은 두마리가 겹쳐있어도 둘다 맞게 하기위해.
                Physics2D.OverlapCircleAll( // 지정한위치와 반지름 안에 있는 Collider2D를 전부 찾는 함수
                    attackPoint.position,
                    attackRange,
                    enemyLayer
                    );

            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyHealth enemyHealth =
                    enemy.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }
    }

    public void DisableMovement()
    {
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("isRunning", false);
        animator.SetFloat("VerticalSpeed", 0f);
        animator.ResetTrigger("Attack");

        animator.Play("idle"); // 죽는 스프라이트가 없어서 임시용

        enabled = false; // MonoBehaviour로부터 물려받은 프로퍼티
    }

    /* -----------------------------------------------------------------------------------*/
    void Start()
    {
        Debug.Log("게임 시작!");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        attackPointStartPosition = attackPoint.localPosition; // 로컬로 해야 플레이어 기준 자식오브젝트위치가됨

    }

    void Update()
    {


        isGround = CheckGround();
        //isSlide = SlideInput();
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
