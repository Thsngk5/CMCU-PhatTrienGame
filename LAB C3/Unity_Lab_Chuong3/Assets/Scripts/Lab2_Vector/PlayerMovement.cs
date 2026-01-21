using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Gizmos Settings")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private float gizmoArrowLength = 2f;
    [SerializeField] private Color gizmoColor = Color.green;
    
    private Vector3 moveDirection;
    private Vector3 normalizedDirection;

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Lấy input từ bàn phím
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D hoặc Left/Right
        float vertical = Input.GetAxisRaw("Vertical");     // W/S hoặc Up/Down

        // Tạo vector di chuyển CHƯA chuẩn hóa
        moveDirection = new Vector3(horizontal, 0, vertical);

        // Chuẩn hóa vector để tránh chạy chéo nhanh hơn
        normalizedDirection = moveDirection.normalized;

        // Di chuyển nhân vật
        transform.Translate(normalizedDirection * moveSpeed * Time.deltaTime, Space.World);

        // Debug log để so sánh
        if (moveDirection != Vector3.zero)
        {
            Debug.Log($"Vector gốc: {moveDirection} | Độ dài: {moveDirection.magnitude:F2}");
            Debug.Log($"Vector chuẩn hóa: {normalizedDirection} | Độ dài: {normalizedDirection.magnitude:F2}");
        }
    }

    // Vẽ Gizmos trong Scene view
    void OnDrawGizmos()
    {
        if (!showGizmos) return;

        // Vẽ vector CHƯA chuẩn hóa (màu đỏ)
        Gizmos.color = Color.red;
        Vector3 startPos = transform.position;
        Vector3 endPosRaw = startPos + moveDirection * gizmoArrowLength;
        Gizmos.DrawLine(startPos, endPosRaw);
        DrawArrowHead(startPos, endPosRaw, Color.red);

        // Vẽ vector ĐÃ chuẩn hóa (màu xanh)
        Gizmos.color = gizmoColor;
        Vector3 endPosNormalized = startPos + normalizedDirection * gizmoArrowLength;
        Gizmos.DrawLine(startPos, endPosNormalized);
        DrawArrowHead(startPos, endPosNormalized, gizmoColor);

        // Vẽ vòng tròn đại diện cho khoảng cách tối đa
        DrawCircle(startPos, gizmoArrowLength, Color.yellow);
    }

    // Vẽ mũi tên
    void DrawArrowHead(Vector3 start, Vector3 end, Color color)
    {
        if (end == start) return;

        Vector3 direction = (end - start).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, direction);
        Vector3 arrowSize = direction * 0.3f;

        Gizmos.color = color;
        Gizmos.DrawLine(end, end - arrowSize + right * 0.15f);
        Gizmos.DrawLine(end, end - arrowSize - right * 0.15f);
    }

    // Vẽ vòng tròn
    void DrawCircle(Vector3 center, float radius, Color color)
    {
        Gizmos.color = color;
        int segments = 50;
        float angle = 0f;

        Vector3 lastPoint = center + new Vector3(radius, 0, 0);

        for (int i = 0; i <= segments; i++)
        {
            angle = i * 360f / segments * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(
                Mathf.Cos(angle) * radius,
                0,
                Mathf.Sin(angle) * radius
            );

            Gizmos.DrawLine(lastPoint, newPoint);
            lastPoint = newPoint;
        }
    }
}
