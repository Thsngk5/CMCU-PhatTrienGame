using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("Movement Pattern")]
    [SerializeField] private MovementPattern pattern = MovementPattern.Circle;
    
    [Header("Circle Settings")]
    [SerializeField] private float circleRadius = 5f;
    [SerializeField] private float circleSpeed = 2f;
    
    [Header("Random Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float changeDirectionInterval = 2f;
    [SerializeField] private float moveRadius = 8f;
    
    private Vector3 centerPosition;
    private float angle = 0f;
    private Vector3 randomDirection;
    private float directionTimer;

    public enum MovementPattern
    {
        Circle,
        Random,
        Manual
    }

    void Start()
    {
        centerPosition = transform.position;
        randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        randomDirection.Normalize();
    }

    void Update()
    {
        switch (pattern)
        {
            case MovementPattern.Circle:
                MoveInCircle();
                break;
            case MovementPattern.Random:
                MoveRandomly();
                break;
            case MovementPattern.Manual:
                MoveManually();
                break;
        }

        // Chuyển đổi pattern bằng phím
        if (Input.GetKeyDown(KeyCode.C))
        {
            pattern = MovementPattern.Circle;
            Debug.Log("Target: Circle movement");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            pattern = MovementPattern.Random;
            Debug.Log("Target: Random movement");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            pattern = MovementPattern.Manual;
            Debug.Log("Target: Manual movement (Arrow keys)");
        }
    }

    void MoveInCircle()
    {
        angle += circleSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;
        transform.position = centerPosition + new Vector3(x, 0, z);
    }

    void MoveRandomly()
    {
        directionTimer += Time.deltaTime;
        
        if (directionTimer >= changeDirectionInterval)
        {
            randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0;
            randomDirection.Normalize();
            directionTimer = 0f;
        }

        Vector3 newPosition = transform.position + randomDirection * moveSpeed * Time.deltaTime;
        
        // Giới hạn trong radius
        if (Vector3.Distance(newPosition, centerPosition) < moveRadius)
        {
            transform.position = newPosition;
        }
        else
        {
            randomDirection = -randomDirection;
        }
    }

    void MoveManually()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(centerPosition, moveRadius);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, moveRadius);
        }
    }
}