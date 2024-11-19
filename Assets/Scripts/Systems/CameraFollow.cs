using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position;

        // Maintain the original Z position
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z);
    }
}