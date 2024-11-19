using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Capture input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        // Movement
        Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;

        // Clamp position using GameBoundary singleton
        newPosition = GameBoundary.Instance.ClampPosition(newPosition, GetComponent<Collider2D>());

        rb.MovePosition(newPosition);
    }
}
