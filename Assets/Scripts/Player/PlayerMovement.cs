using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector2 newPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }
}