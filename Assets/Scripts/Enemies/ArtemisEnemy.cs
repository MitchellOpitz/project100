using UnityEngine;

public class ArtemisEnemy : Enemy
{
    public override void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
