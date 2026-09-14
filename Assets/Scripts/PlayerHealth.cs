using UnityEngine;
using TMPro; // 플레이어 체력 보여주기위해.


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    public TMP_Text hpText;

    void Start()
    {
        currentHealth = maxHealth;

        //Debug.Log("현재 체력 : " + currentHealth);
        hpText.text = "HP : " + currentHealth;

    }

    private void Die()
    {
        isDead = true;

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.DisableMovement(); // PlayerMovement를 비활성화해서 움직이지 않도록.
        }

        Debug.Log("죽음");
    }

    public void TakeDamage(int damage)
    {
        if (isDead) // 데미지입는걸 죽어서도 계속 호출안되게 하기위해.
        {
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Mathf.Clamp(값, 최솟값, 최댓값) 체력 음수방지.

        hpText.text = "HP : " + currentHealth;

        //Debug.Log("현재 체력 :" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
