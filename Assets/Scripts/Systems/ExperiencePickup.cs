using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    [SerializeField] private float colorCycleSpeed = 2f; // Speed of the hue change
    private SpriteRenderer spriteRenderer;
    private float hue;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hue = Random.value; // Start at a random hue for variation
    }

    private void Update()
    {
        // Cycle through the hue
        hue += colorCycleSpeed * Time.deltaTime;
        if (hue > 1f) hue -= 1f; // Wrap around to keep hue in the range [0, 1]

        // Convert hue to RGB and apply to the sprite
        spriteRenderer.color = Color.HSVToRGB(hue, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the experience orb
        }
    }
}
