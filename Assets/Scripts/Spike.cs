using UnityEngine;

public class Spike : MonoBehaviour
{

    public int damage = 20;
    public float horizontalKnockback = 3f;
    public float verticalKnockback = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();


        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        if (rb != null)
        {
            Vector2 knockbackDirection =
                (other.transform.position - transform.position).normalized;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // 점프해서 가시 밟을때처리용

            rb.AddForce(
                new Vector2(knockbackDirection.x * horizontalKnockback,
                        verticalKnockback),
                ForceMode2D.Impulse); // Impulse는 한번에 힘주는것
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
