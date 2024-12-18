using UnityEngine;

public class VerticalPatrolMovement : MonoBehaviour
{
    [SerializeField] private float bottomPoint = -5f;
    [SerializeField] private float topPoint = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool startingDirectionIsUp = true;

    private bool movingUp;
    private Rigidbody2D rb;
    private Vector2 startingPosition;

    private void Awake()
    {
        startingPosition = transform.position;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movingUp = startingDirectionIsUp;
    }

    void FixedUpdate()
    {
        MoveVertically();
    }

    void MoveVertically()
    {
        Vector2 position = rb.position;

        float actualTop = startingPosition.y + topPoint;
        float actualBottom = startingPosition.y + bottomPoint;

        if (movingUp)
        {
            position.y += speed * Time.fixedDeltaTime;
            if (position.y >= actualTop)
            {
                position.y = actualTop;
                movingUp = false;
            }
        }
        else
        {
            position.y -= speed * Time.fixedDeltaTime;
            if (position.y <= actualBottom)
            {
                position.y = actualBottom;
                movingUp = true;
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
        Gizmos.color = Color.yellow;

        Vector3 startPos = startingPosition + new Vector2(0, bottomPoint);
        Vector3 endPos = startingPosition + new Vector2(0, topPoint);
        Gizmos.DrawLine(startPos, endPos);
        Gizmos.DrawSphere(startPos, 0.2f);
        Gizmos.DrawSphere(endPos, 0.2f);

        Gizmos.color = originalColor;
    }
}
