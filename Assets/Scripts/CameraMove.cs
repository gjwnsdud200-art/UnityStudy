using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float followSpeed = 2f;

    void LateUpdate()
    {
        Vector3 pos = new Vector3(
            player.position.x, 
            player.position.y , 
            -10
            );
        transform.position = Vector3.Lerp(
            transform.position,
            pos,
            followSpeed * Time.deltaTime
            ) ;

        
    }


}
