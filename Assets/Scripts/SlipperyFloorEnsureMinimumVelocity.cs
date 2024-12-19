using UnityEngine;

public class SlipperyFloorEnsureMinimumVelocity : MonoBehaviour
{
    [SerializeField] private float minimumVelocity = 5f;
    [SerializeField] private float defaultDirection = 1f; 

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 currentVelocity = playerRb.velocity;

                // If the player is stationary, push them towards the right. Should never really happen tho
                if (Mathf.Abs(currentVelocity.x) < 0.01f) // Threshold for "almost zero"
                {
                    playerRb.velocity = new Vector2(defaultDirection * minimumVelocity, currentVelocity.y);
                }
                //if the player is really slow, give them a minimum velocity
                else if (Mathf.Abs(currentVelocity.x) < minimumVelocity)
                {
                    float newVelocityX = Mathf.Sign(currentVelocity.x) * minimumVelocity;
                    playerRb.velocity = new Vector2(newVelocityX, currentVelocity.y);
                }
            }
        }
    }
}
