using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform[] backgrounds;
    public Transform cameraTransform;


    private float backgroundWidth;
   
    void Start()
    {
        SpriteRenderer spriteRenderer = backgrounds[0].GetComponent<SpriteRenderer>();

        backgroundWidth = spriteRenderer.bounds.size.x;

        // 배경을 처음에 가로로 정확히 배치
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position = new Vector3(
                backgrounds[0].position.x + backgroundWidth * i,
                backgrounds[0].position.y,
                backgrounds[0].position.z
                );
        }
    }

   
    void Update()
    {
        float leftMostX = backgrounds[0].position.x;
        float rightMostX = backgrounds[0].position.x;

        foreach (Transform background in backgrounds)
        {
            if (background.position.x < leftMostX)
                leftMostX = background.position.x;

            if (background.position.x > rightMostX)
                rightMostX = background.position.x;
        }

        foreach (Transform background in backgrounds)
        {

            if (cameraTransform.position.x - background.position.x
                > backgroundWidth)
            {
                background.position = new Vector3(
                    rightMostX + backgroundWidth,
                    background.position.y,
                    background.position.z
                    );

                rightMostX = background.position.x;
            }

            else if (background.position.x - cameraTransform.position.x
                    > backgroundWidth)
            {
                background.position = new Vector3(
                    leftMostX - backgroundWidth,
                    background.position.y,
                    background.position.z
                    );

                leftMostX = background.position.x;
            }
        }

        
    }
}
