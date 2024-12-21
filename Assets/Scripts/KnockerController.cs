using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script works both on trigger and non-trigger colliders
public class KnockerController : MonoBehaviour
{

    [SerializeField] private float knockbackForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyKnockback(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyKnockback(collision.gameObject);
        }
    }

    private void ApplyKnockback(GameObject player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 impactDirection = (playerRb.position - (Vector2)transform.position).normalized;
            playerRb.velocity = impactDirection * knockbackForce; 
        }
    }
}
