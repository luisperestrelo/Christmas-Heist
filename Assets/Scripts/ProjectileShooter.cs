using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private bool aimAtPlayer = false;
    [SerializeField] private Vector2 fireDirection = Vector2.left;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float lifetime = 3f;

    private float timer;
    private Transform playerTransform;

    void Start()
    {
        timer = fireInterval;
        if (aimAtPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) 
                playerTransform = player.transform;
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = fireInterval;
        }
    }

    void Shoot()
    {
        Vector2 direction = fireDirection;

        if (aimAtPlayer && playerTransform != null)
        {
            direction = (playerTransform.position - transform.position).normalized;
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
        if (projRb)
        {
            projRb.velocity = direction * projectileSpeed;
        }

        Destroy(projectile, lifetime);
    }
}
