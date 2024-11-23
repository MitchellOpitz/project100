using UnityEngine;

public class DionysusEnemy : Enemy
{
    public float radius = 2f;
    public float rotationSpeed = 2f;

    private Vector3 centralPoint;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        centralPoint = transform.position;
    }

    public override void Move()
    {
        centralPoint += transform.up * speed * Time.deltaTime;

        angle += rotationSpeed * Time.deltaTime;
        float xOffset = Mathf.Sin(angle) * radius;
        float yOffset = Mathf.Cos(angle) * radius;

        transform.position = centralPoint + transform.rotation * new Vector3(xOffset, yOffset, 0);
    }
}
