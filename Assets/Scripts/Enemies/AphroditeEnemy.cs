
using UnityEngine;

public class AphroditeEnemy : Enemy
{
    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    public override void Move()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
