using UnityEngine;

public class ArtemisEnemy : Enemy
{
    public override void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
