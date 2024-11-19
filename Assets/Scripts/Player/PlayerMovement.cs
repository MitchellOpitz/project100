using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private Rigidbody2D rb;
    private GameBoundary gameBoundary;
    private BoxCollider2D playerCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameBoundary = FindObjectOfType<GameBoundary>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // Calculate the desired new position
        Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;

        // Get player's half width and height
        float halfWidth = playerCollider.bounds.extents.x;
        float halfHeight = playerCollider.bounds.extents.y;

        // Clamp the position within the game boundary, accounting for player size
        newPosition.x = Mathf.Clamp(newPosition.x, gameBoundary.GetMinX() + halfWidth, gameBoundary.GetMaxX() - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, gameBoundary.GetMinY() + halfHeight, gameBoundary.GetMaxY() - halfHeight);

        rb.MovePosition(newPosition);

        // Facing the mouse
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        // Rotate only on the Z axis
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}