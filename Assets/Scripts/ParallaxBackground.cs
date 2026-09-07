using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform;

    public float parallaxEffect = 0.3f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(delta.x * parallaxEffect, 0, 0);

        lastCameraPosition = cameraTransform.position;
    }
}
