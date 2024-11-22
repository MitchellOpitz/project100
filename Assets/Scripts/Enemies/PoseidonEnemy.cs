using UnityEngine;

public class PoseidonEnemy : Enemy
{
    private float amplitude = 0.02f; // Adjust wave height
    private float frequency = 3f;  // Adjust wave speed

    public override void Move()
    {
        // Calculate horizontal sine wave offset
        float horizontalOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Determine perpendicular movement based on rotation
        Vector3 perpendicularOffset = transform.right * horizontalOffset;

        // Calculate forward movement
        Vector3 forwardMovement = transform.up * speed * Time.deltaTime;

        // Combine forward movement with the sine wave offset
        transform.Translate(perpendicularOffset + forwardMovement, Space.World);
    }
}
