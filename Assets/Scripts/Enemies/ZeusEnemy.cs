using UnityEngine;

public class ZeusEnemy : Enemy
{
    public float amplitude = 2f;
    public float frequency = 2f;

    private Vector3 originalPosition;
    private float oscillationOffset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    public override void Move()
    {
        Vector3 forwardMovement = transform.up * speed * Time.deltaTime;
        originalPosition += forwardMovement;

        oscillationOffset += frequency * Time.deltaTime;
        float horizontalOffset = Mathf.PingPong(oscillationOffset, amplitude * 2) - amplitude;

        Vector2 localOffset = new Vector3(horizontalOffset, 0, 0);
        transform.position = originalPosition + transform.rotation * localOffset;
    }
}
