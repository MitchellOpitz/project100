using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Camera cam;
    private ScreenShake screenShake;  // Reference to the CameraShake component

    private void Start()
    {
        cam = GetComponent<Camera>();
        screenShake = GetComponent<ScreenShake>();  // Get the CameraShake component
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position;

        // Apply screen shake if it's active
        if (screenShake != null && screenShake.shakeDuration > 0)
        {
            desiredPosition += new Vector3(
                Random.Range(-screenShake.shakeIntensity, screenShake.shakeIntensity),
                Random.Range(-screenShake.shakeIntensity, screenShake.shakeIntensity),
                0);
        }

        // Camera half dimensions for clamping
        float camHalfWidth = cam.orthographicSize * Screen.width / Screen.height;
        float camHalfHeight = cam.orthographicSize;

        // Clamp position using GameBoundary singleton
        desiredPosition = GameBoundary.Instance.ClampPosition(desiredPosition, camHalfWidth, camHalfHeight);

        // Maintain original Z position
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z);
    }
}
