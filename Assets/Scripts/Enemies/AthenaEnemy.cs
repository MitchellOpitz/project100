using UnityEngine;

public class AthenaEnemy : Enemy
{
    public float travelDistance = 5f;
    public float speed = 3f;

    private enum MovementState { MovingVertical, MovingHorizontalLeft, MovingVerticalAgain, MovingHorizontalRight }
    private MovementState currentState = MovementState.MovingVertical;
    private float distanceTraveled = 0f;

    // Start is called before the first frame update
    void Start()
    {
        distanceTraveled = 0f;
    }

    public override void Move()
    {
        switch (currentState)
        {
            case MovementState.MovingVertical:
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                distanceTraveled += speed * Time.deltaTime;

                if (distanceTraveled >= travelDistance)
                {
                    currentState = MovementState.MovingHorizontalLeft;
                    distanceTraveled = 0f;
                }
                break;
            case MovementState.MovingHorizontalLeft:
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                distanceTraveled += speed * Time.deltaTime;

                if (distanceTraveled >= travelDistance)
                {
                    currentState = MovementState.MovingVerticalAgain;
                    distanceTraveled = 0f;
                }
                break;
            case MovementState.MovingVerticalAgain:
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                distanceTraveled += speed * Time.deltaTime;

                if (distanceTraveled >= travelDistance)
                {
                    currentState = MovementState.MovingHorizontalRight;
                    distanceTraveled = 0f;
                }
                break;
            case MovementState.MovingHorizontalRight:
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                distanceTraveled += speed * Time.deltaTime;

                if (distanceTraveled >= travelDistance)
                {
                    currentState = MovementState.MovingVertical;
                    distanceTraveled = 0f;
                }
                break;
        }
    }
}
