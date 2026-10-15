using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        float ImpactStrength = collision.relativeVelocity.magnitude;

        Debug.Log("충돌 강도 : " + ImpactStrength);

        if (ImpactStrength >= 8f)
        {
            Debug.Log("완전파괴");
        }
        else if (ImpactStrength >= 4f)
        {
            Debug.Log("일부 파손");
        }
        else
        {
            Debug.Log("파괴되지않음");
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
