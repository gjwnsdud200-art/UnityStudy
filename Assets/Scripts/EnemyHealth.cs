using UnityEngine;
using UnityEngine.UI; // C# 자체의 namespace 규칙때문에 UI를 한번 더 쓰는것
using System.Collections; // IEnumerator라는 타입이 System.Collections 네임스페이스에 있기 때문

// using XXX;가 나오면 "이 스크립트에서 XXX라는 네임스페이스에 있는 클래스 이름들을 짧게 쓰겠다"라고 이해

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Image healthFill; // 적 체력 UI용

    private SpriteRenderer sr;
    private Color originalColor;


    public GameObject deathEffect;

    void UpdateHealthBar()
    {
        float healthRatio =
            (float)currentHealth / maxHealth; // currentHealth와 maxHealth가 둘 다 int라서 정확한 소수 계산을 위해 (float)로 변환

        healthFill.transform.localScale =
            new Vector3(
                healthRatio,
                1f,
                1f
                );
    }

    void Start()
    {
        currentHealth = maxHealth;

        sr = GetComponent<SpriteRenderer>();

        originalColor = sr.color; // 적의 원래 색상을 저장
    }

    IEnumerator HitFlash()
    {
        sr.color = Color.red;

        yield return new WaitForSeconds(0.1f); // 0.1초 동안 기다림

        sr.color = originalColor;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        currentHealth =
            Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        StartCoroutine(HitFlash());

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
