using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float aimDistance = 10f;
    [SerializeField] private LayerMask anchorLayer; 
    [SerializeField] private LineRenderer aimLine; //might change for an actual sprite or dots or smth later

    public LayerMask AnchorLayer {get => anchorLayer;}
    private Rigidbody2D rb;
    private Vector2 aimDirection;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleAim();
    }

    void HandleAim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;

        aimDirection = mousePos - transform.position;
        aimDirection.Normalize();

        if (aimLine != null)
        {
            aimLine.SetPosition(0, playerPos);
            aimLine.SetPosition(1, playerPos + (Vector2)aimDirection * aimDistance);
        }
    }

    public Vector2 GetAimDirection()
    {
        return aimDirection;
    }
}
