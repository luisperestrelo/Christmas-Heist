using UnityEngine;

//This script works both on trigger and non-trigger colliders
public class GrappleDetacher : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DetachGrapple(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DetachGrapple(collision.gameObject);
        }
    }

    private void DetachGrapple(GameObject player)
    {
        GrappleController grapple = player.GetComponent<GrappleController>();
        if (grapple != null)
        {
            grapple.ForceDetachGrapple();
        }
    }
}


