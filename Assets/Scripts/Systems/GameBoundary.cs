using UnityEngine;

public class GameBoundary : MonoBehaviour
{
    public BoxCollider2D gameBounds;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        minX = gameBounds.bounds.min.x;
        maxX = gameBounds.bounds.max.x;
        minY = gameBounds.bounds.min.y;
        maxY = gameBounds.bounds.max.y;
    }

    public float GetMinX() { return minX; }
    public float GetMaxX() { return maxX; }
    public float GetMinY() { return minY; }
    public float GetMaxY() { return maxY; }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameBounds.bounds.center, gameBounds.bounds.size);
    }
}