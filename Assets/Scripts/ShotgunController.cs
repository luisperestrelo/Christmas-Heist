using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private float backwardsForce = 10f;
    [SerializeField] private KeyCode shootKey = KeyCode.F;
    [SerializeField] private float cooldownTime = 2f;


    private Rigidbody2D playerRb;
    private bool wantToShoot = false;
    private Vector2 shootDirection;
    private bool canShoot = true;

    private float lastShotTime = -Mathf.Infinity;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            wantToShoot = true;
        }
    }

    void FixedUpdate()
    {
        if (wantToShoot)
        {
            if (canShoot)
                Shoot();
            wantToShoot = false;
        }
    }

    private void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;

        shootDirection = mousePos - transform.position;
        shootDirection.Normalize();

        playerRb.AddForce(-shootDirection * backwardsForce, ForceMode2D.Impulse);

        lastShotTime = Time.time;
        StartCoroutine(ShotCooldown());
    }

    IEnumerator ShotCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    // 1 means it's ready.
    public float GetCooldownProgress()
    {
        float elapsed = Time.time - lastShotTime;

        float fraction = elapsed / cooldownTime;

        return Mathf.Clamp01(fraction); // Ensures it never goes above 1
    }
}
