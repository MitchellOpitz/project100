using UnityEngine;

public class GameBoundary : MonoBehaviour
{
    public static GameBoundary Instance { get; private set; }

    public BoxCollider2D gameBounds;

    private float minX, maxX, minY, maxY;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        minX = gameBounds.bounds.min.x;
        maxX = gameBounds.bounds.max.x;
        minY = gameBounds.bounds.min.y;
        maxY = gameBounds.bounds.max.y;
    }

    public float GetMinX() => minX;
    public float GetMaxX() => maxX;
    public float GetMinY() => minY;
    public float GetMaxY() => maxY;

    public Vector2 ClampPosition(Vector2 position, Collider2D collider)
    {
        float halfWidth = collider.bounds.extents.x;
        float halfHeight = collider.bounds.extents.y;

        position.x = Mathf.Clamp(position.x, minX + halfWidth, maxX - halfWidth);
        position.y = Mathf.Clamp(position.y, minY + halfHeight, maxY - halfHeight);

        return position;
    }

    public Vector3 ClampPosition(Vector3 position, float halfWidth, float halfHeight)
    {
        position.x = Mathf.Clamp(position.x, minX + halfWidth, maxX - halfWidth);
        position.y = Mathf.Clamp(position.y, minY + halfHeight, maxY - halfHeight);

        return position;
    }

    public bool IsOutOfBounds(Vector2 position, float offset = 0f)
    {
        return position.x < (minX - offset) || position.x > (maxX + offset) ||
               position.y < (minY - offset) || position.y > (maxY + offset);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameBounds.bounds.center, gameBounds.bounds.size);
    }
}
