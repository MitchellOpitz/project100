using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private GameBoundary gameBoundary;
    private Camera cam;

    void Start()
    {
        gameBoundary = FindObjectOfType<GameBoundary>();
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position;

        // Calculate camera's half width and height
        float camHalfWidth = cam.orthographicSize * Screen.width / Screen.height;
        float camHalfHeight = cam.orthographicSize;

        // Clamp camera position within the game boundary
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, gameBoundary.GetMinX() + camHalfWidth, gameBoundary.GetMaxX() - camHalfWidth);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, gameBoundary.GetMinY() + camHalfHeight, gameBoundary.GetMaxY() - camHalfHeight);

        // Maintain the original Z position
        transform.position = new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z);
    }
}