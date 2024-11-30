using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private int moveSpeedRank;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeedRank = 0;
    }

    private void OnEnable()
    {
        UpgradeManager.OnUpgradeSelected += OnUpgradeSelected;
    }

    private void OnDisable()
    {
        UpgradeManager.OnUpgradeSelected -= OnUpgradeSelected;
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
        float finalMoveSpeed = speed * (1 + moveSpeedRank * 0.1f);
        Debug.Log($"Final Move Speed: {finalMoveSpeed}");
        Vector2 newPosition = rb.position + moveDirection * finalMoveSpeed * Time.fixedDeltaTime;

        // Clamp position using GameBoundary singleton
        newPosition = GameBoundary.Instance.ClampPosition(newPosition, GetComponent<Collider2D>());

        rb.MovePosition(newPosition);
    }

    private void OnUpgradeSelected()
    {
        moveSpeedRank = UpgradeManager.GetUpgradeRank("Move Speed");
    }
}
