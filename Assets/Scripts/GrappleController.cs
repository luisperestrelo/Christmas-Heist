using UnityEngine;

public class GrappleController : MonoBehaviour
{
    [SerializeField] private float maxGrappleDistance = 15f;
    [SerializeField] private KeyCode grappleKey = KeyCode.Space;

    [SerializeField] private float releaseBoost = 1.1f;
    [SerializeField] private LineRenderer ropeLine;

    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float groundCheckDistance = 0.1f;

    private Vector2 grapplePoint; // The point where the grapple connected

    private DistanceJoint2D grappleJoint;
    private PlayerController playerController;
    private Rigidbody2D rb;
    private GameObject attachedObject;

    private bool wantToGrapple = false;
    private bool wantToRelease = false;
    private bool isGrappling = false;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();

        grappleJoint = GetComponent<DistanceJoint2D>();
        grappleJoint.enabled = false;

        ropeLine.enabled = false; // hide line when not grappling

        SetInitialAnchor();

    }


    void Update()
    {
        // for now, the only point of checking ground is so the character does a small jump when grappling from a grounded position
        CheckIfGrounded();

        if (Input.GetKeyDown(grappleKey))
        {
            wantToGrapple = true;
        }

        if (Input.GetKeyUp(grappleKey))
        {
            wantToRelease = true;
        }

        if (isGrappling)
        {
            UpdateRopeLine();
        }
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
    }

    void FixedUpdate()
    {
        if (wantToGrapple)
        {
            AttemptGrapple();
            wantToGrapple = false;
        }

        if (wantToRelease)
        {
            ReleaseGrapple();
            wantToRelease = false;
        }
    }

    void SetInitialAnchor()
    {
        grappleJoint.enabled = true;

        isGrappling = true;
        ropeLine.enabled = true;
        attachedObject = grappleJoint.connectedBody.gameObject;

    }

    void AttemptGrapple()
    {

        Vector2 aimDir = playerController.GetAimDirection();
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, aimDir, maxGrappleDistance, playerController.AnchorLayer);

        if (hit.collider != null)
        {
            // We hit an anchor point
            grappleJoint.enabled = true;


            grappleJoint.connectedBody = hit.collider.attachedRigidbody;


            float distanceToAnchor = Vector2.Distance(origin, hit.point);
            grappleJoint.distance = distanceToAnchor;

            //grapplePoint = hit.point;

            attachedObject = hit.collider.gameObject;

            grapplePoint = hit.collider.transform.position;
            isGrappling = true;
            ropeLine.enabled = true;

            if (isGrounded) //small jump
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

        }
    }

    void ReleaseGrapple()
    {
        if (!isGrappling)
            return;


        // small boost at the moment of release:
        rb.velocity = rb.velocity * releaseBoost;
        grappleJoint.enabled = false;
        grappleJoint.connectedBody = null;
        isGrappling = false;
        ropeLine.enabled = false;
    }

    public void ForceDetachGrapple()
    {
        if (!isGrappling)
        {
            Debug.Log("Forced Detach called when not grappled");
            return;
        }

        grappleJoint.enabled = false;
        grappleJoint.connectedBody = null;
        isGrappling = false;
        ropeLine.enabled = false;
    }

    void UpdateRopeLine()
    {
        if (!isGrappling) return;

        // The rope should go from player position to the grapplePoint
        ropeLine.positionCount = 2;
        ropeLine.SetPosition(0, transform.position);
        ropeLine.SetPosition(1, attachedObject.transform.position);
    }
}
