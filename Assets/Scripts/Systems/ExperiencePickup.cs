using UnityEngine;
using System;

public class ExperiencePickup : MonoBehaviour
{
    public static event Action<ExperiencePickup> OnExperiencePickedUp; // Event for experience pickup

    [SerializeField] private float colorCycleSpeed = 2f; // Speed of the hue change
    [SerializeField] private float lifetime = 3f; // Time before fading starts
    [SerializeField] private float fadeDuration = 2f; // Time it takes to fade out completely

    private SpriteRenderer spriteRenderer;
    private float hue;
    private float elapsedTime = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hue = UnityEngine.Random.value; // Start at a random hue for variation
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        CycleHue();
        HandleFading();
    }

    private void CycleHue()
    {
        hue += colorCycleSpeed * Time.deltaTime;
        if (hue > 1f) hue -= 1f; // Wrap around to keep hue in the range [0, 1]
        Color currentColor = Color.HSVToRGB(hue, 1f, 1f);
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, spriteRenderer.color.a); // Preserve alpha
    }

    private void HandleFading()
    {
        if (elapsedTime < lifetime) return;

        float fadeProgress = (elapsedTime - lifetime) / fadeDuration;
        Color currentColor = spriteRenderer.color;
        currentColor.a = Mathf.Lerp(1f, 0f, fadeProgress);
        spriteRenderer.color = currentColor;

        if (fadeProgress >= 1f)
        {
            Destroy(gameObject); // Destroy after fade out
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerPickupEvent();
            Destroy(gameObject); // Destroy immediately on player pickup
        }
    }

    private void TriggerPickupEvent()
    {
        OnExperiencePickedUp?.Invoke(this);
    }
}
