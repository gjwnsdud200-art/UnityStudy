using UnityEngine;

public class EffectDestroy : MonoBehaviour
{

    public float destroyTime = 1f;
   
    void Start()
    {
        Destroy(gameObject, destroyTime); // 이펙트가 생성된 후 일정 시간이 지나면 자기 자신을 삭제
    }

    void Update()
    {
        
    }
}
