using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
        Debug.Log("게임 시작!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스페이스바가 눌렀습니다!");
        }
    }
}
