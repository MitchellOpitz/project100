using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperienceMagnet : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    [SerializeField] private float pullSpeed = 5f; // Speed at which experience moves towards the magnet

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Experience")
        {
            Debug.Log("Touched experience.");
            StartCoroutine(PullExperience(collision.transform));
        }
    }

    private IEnumerator PullExperience(Transform experience)
    {
        while (experience != null && Vector2.Distance(experience.position, transform.position) > 0.1f)
        {
            // Move the experience object towards the magnet
            experience.position = Vector2.MoveTowards(experience.position, transform.position, pullSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        if (experience != null)
        {
            // Snap the experience to the magnet's position when close enough
            experience.position = transform.position;
        }
    }
}
