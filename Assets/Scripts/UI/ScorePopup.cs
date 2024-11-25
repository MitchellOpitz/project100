using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private float moveUpDistance = 1f;
    [SerializeField] private float fadeOutTime = 1f;

    private TextMeshProUGUI textMesh;
    private Color initialColor;
    private Vector3 initialPosition;
    private float elapsedTime;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        initialColor = textMesh.color;
        initialPosition = transform.position;
    }

    public void Initialize(int score)
    {
        textMesh.text = $"+{score}";
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / fadeOutTime;

        // Move up
        transform.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * moveUpDistance, progress);

        // Fade out
        textMesh.color = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(initialColor.a, 0, progress));

        // Destroy when animation is complete
        if (progress >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
