using UnityEngine;

public class ChimneyTrigger : MonoBehaviour
{
    private bool triggered = false; // To prevent scoring multiple times
    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            // Player passed over the chimney
            triggered = true;

            spriteRenderer.color = Color.green;
            
            //Particle effects? ScoreTracker? TODO
        }
    }
}
