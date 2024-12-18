using UnityEngine;

public class AnchorMover : MonoBehaviour
{
    [SerializeField] private float leftPoint = -5f;
    [SerializeField] private float rightPoint = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool startingDirectionisRight = true;

    private bool movingRight;
    private Rigidbody2D rb;

    private Vector3 startingPosition;

    private void Awake()
    {
        startingPosition = transform.position;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movingRight = startingDirectionisRight;
    }

    void FixedUpdate()
    {
        MoveAnchor();
    }

    void MoveAnchor()
    {
        Vector2 position = rb.position;

        if (movingRight)
        {
            position.x += speed * Time.deltaTime;
            if (position.x >= rightPoint)
            {
                position.x = rightPoint;
                movingRight = false;
            }
        }
        else
        {
            position.x -= speed * Time.deltaTime;
            if (position.x <= leftPoint)
            {
                position.x = leftPoint;
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

        Vector3 startPosition = startingPosition + new Vector3(leftPoint, 0, 0);
        Vector3 endPosition = startingPosition + new Vector3(rightPoint, 0, 0);
        Gizmos.DrawLine(startPosition, endPosition);

        Gizmos.DrawSphere(startPosition, 0.2f); // Start sphere
        Gizmos.DrawSphere(endPosition, 0.2f);   // End sphere

        Gizmos.color = originalColor;
    }
}
