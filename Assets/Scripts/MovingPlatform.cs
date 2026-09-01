using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointB;
    public float speed = 2f;

    private Vector3 pointA;
    private bool movingToB = true;

    void Start()
    {
        pointA = transform.position;
    }

    void Update()
    {
        if (movingToB)
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    pointB.position,
                    speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
                movingToB = false;
        }
        else
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    pointA,
                    speed * Time.deltaTime
                    );

            if (Vector3.Distance(transform.position, pointA) < 0.01f)
                movingToB = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
    

