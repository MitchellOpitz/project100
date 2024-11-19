using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position;

        // Camera half dimensions
        float camHalfWidth = cam.orthographicSize * Screen.width / Screen.height;
        float camHalfHeight = cam.orthographicSize;

        // Clamp position using GameBoundary singleton
        desiredPosition = GameBoundary.Instance.ClampPosition(desiredPosition, camHalfWidth, camHalfHeight);

        // Maintain original Z position
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z);
    }
}
