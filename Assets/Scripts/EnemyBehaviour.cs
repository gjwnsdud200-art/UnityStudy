using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float moveSpeed = 2f;
    public float patrolDistance = 3f;

    private float startX; // 적이 처음 시작한 X위치기억용.
    private int direction = 1;

    public Transform player; //  플레이어 실제 위치를 알기위한 참조
    public float chaseDistance = 5f;
    public float maxChaseDistance = 8f;

    private bool chaseEnd = false;



    public int damage = 20; // 충돌시 데미지

    private void OnCollisionEnter2D(Collision2D collision) // 부딫히면 데미지받게
    {
        PlayerHealth playerHealth =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    /*
     Unity Physics
     ↓
"두 Collider2D가 충돌했다!"
     ↓
해당 MonoBehaviour에
OnCollisionEnter2D가 있는지 확인
     ↓
있으면 Unity가 호출
     */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        startX = transform.position.x;
        chaseEnd = false;
    }

    void Patrol()
    {
        
        transform.position +=
            Vector3.right * direction * moveSpeed * Time.deltaTime;

      
        if (Mathf.Abs(transform.position.x - startX) < 0.1f) // 원래자리로 돌아오면 다시 Chase 할수있게.
        {
            chaseEnd = false;
        }
        
        if (transform.position.x >= startX + patrolDistance)
        {
            direction = -1; // 방향 반대로.
            sr.flipX = true; // 스프라이트 좌우반전
        }
        else if (transform.position.x <= startX - patrolDistance)
        {
            direction = 1;
            sr.flipX = false;
        }
    }

    void ChasePlayer()
    {
        if (player.position.x > transform.position.x)
        {
            direction = 1;
            sr.flipX = false;
        }
        else 
        {
            direction = -1;
            sr.flipX = true;
        }

        transform.position +=
            Vector3.right * direction * moveSpeed * Time.deltaTime;

        
    }

    
    void Update()
    {
        float distanceToPlayer =
            Vector2.Distance(transform.position, player.position);

        float distanceFromStart =   // 계속해서 Chase하는걸 방지하기위해 일정거리 이상하면 다시 Patrol하게
            Mathf.Abs(transform.position.x - startX); // Mathf.Abs를 통해 오른쪽이건 왼쪽이건 음수라도 거리만 알게.



        if (distanceToPlayer <= chaseDistance && distanceFromStart <= maxChaseDistance && !chaseEnd)
        {
            ChasePlayer();
            
        }
        else 
        {
            if (distanceFromStart >= maxChaseDistance) // 최대치에 닿으면 Chase그만하고 원래자리로 돌아가게.
            {
                chaseEnd = true;
            }
            Patrol();
        }


       
    }
}
