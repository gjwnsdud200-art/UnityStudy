using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public float jumpForce = 8f;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGround;

    void Start()
    {
        Debug.Log("게임 시작!");
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
            );
        float move = Input.GetAxisRaw("Horizontal");

        //transform.position += Vector3.right * move * speed * Time.deltaTime;

        //rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        rb.AddForce(Vector2.right * move * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGround) 
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
