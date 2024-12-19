using UnityEngine;

//This script works both on trigger and non-trigger colliders
public class VelocityResetter : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetPlayerVelocity(collision.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetPlayerVelocity(collision.gameObject);
        }
    }

    private void ResetPlayerVelocity(GameObject player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
        }
    }
}
