using UnityEngine;

public class AnchorMover : MonoBehaviour
{
    [SerializeField] private float leftPoint = -5f;
    [SerializeField] private float rightPoint = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool startingDirectionIsRight = true;

    private bool movingRight;
    private Rigidbody2D rb;
    private Vector2 startingPosition;

    private void Awake()
    {
        startingPosition = transform.position;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movingRight = startingDirectionIsRight;
    }

    void FixedUpdate()
    {
        MoveAnchor();
    }

    void MoveAnchor()
    {
        Vector2 position = rb.position;

        float actualLeft = startingPosition.x + leftPoint;
        float actualRight = startingPosition.x + rightPoint;

        if (movingRight)
        {
            position.x += speed * Time.fixedDeltaTime;
            if (position.x >= actualRight)
            {
                position.x = actualRight;
                movingRight = false;
            }
        }
        else
        {
            position.x -= speed * Time.fixedDeltaTime;
            if (position.x <= actualLeft)
            {
                position.x = actualLeft;
                movingRight = true;
            }
        }

        rb.MovePosition(position);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            startingPosition = transform.position;
        }

        Color originalColor = Gizmos.color;

        Gizmos.color = Color.green;

        Vector3 startPosition = startingPosition + new Vector2(leftPoint, 0);
        Vector3 endPosition = startingPosition + new Vector2(rightPoint, 0);
        Gizmos.DrawLine(startPosition, endPosition);

        Gizmos.DrawSphere(startPosition, 0.2f); // Start sphere
        Gizmos.DrawSphere(endPosition, 0.2f);   // End sphere

        Gizmos.color = originalColor;
    }
}
