using UnityEngine;

//This script works both on trigger and non-trigger colliders
//But it doesn't really make sense on non-trigger tbh, but im keeping the code either way for now
public class VelocityChanger : MonoBehaviour
{
    [SerializeField] private float velocityBoost = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BoostPlayerVelocity(collision.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BoostPlayerVelocity(collision.gameObject);
        }
    }

    private void BoostPlayerVelocity(GameObject player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Debug.Log("Velocity went from " + playerRb.velocity);
            playerRb.velocity *= velocityBoost;
            Debug.Log("to " + playerRb.velocity);
        }
    }
}
