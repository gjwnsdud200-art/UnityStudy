using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;


    public GameObject deathEffect;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        currentHealth =
            Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("적 체력 : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("적 사망");

        Instantiate( // 샐로운 게임오브젝트를 만들어내는 함수
            deathEffect,
            transform.position,
            Quaternion.identity // 회전없이 기본방향으로
            );

        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
