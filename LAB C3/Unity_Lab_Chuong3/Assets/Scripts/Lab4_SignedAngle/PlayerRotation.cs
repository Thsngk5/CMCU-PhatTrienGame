using UnityEngine;
using TMPro; // Nếu dùng TextMeshPro

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 10f;
    
    [Header("Mode")]
    [SerializeField] private bool followMouse = true; // true = theo chuột, false = theo target
    [SerializeField] private Transform target; // Target để follow (nếu không theo chuột)
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI angleText; // Hoặc dùng Text nếu dùng Legacy
    
    private Camera mainCamera;
    private float currentAngle = 0f;
    
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        Vector3 targetPosition;
        
        if (followMouse)
        {
            // Lấy vị trí chuột trong world space
            targetPosition = GetMouseWorldPosition();
        }
        else
        {
            // Sử dụng target object
            if (target == null)
            {
                Debug.LogWarning("Target not assigned!");
                return;
            }
            targetPosition = target.position;
        }
        
        // Tính hướng từ player đến target
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; // Giữ xoay trên mặt phẳng ngang
        
        if (direction.magnitude > 0.1f) // Tránh tính toán khi quá gần
        {
            // Tính signed angle giữa forward hiện tại và hướng đến target
            currentAngle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            
            // Tạo rotation mới
            Quaternion targetRotation = Quaternion.Euler(0, currentAngle, 0);
            
            // Xoay mượt đến rotation đích
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            // Cập nhật UI
            UpdateAngleUI();
        }
    }
    
    Vector3 GetMouseWorldPosition()
    {
        // Tạo ray từ camera qua vị trí chuột
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        // Tạo plane tại độ cao của player
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        
        // Kiểm tra ray có chạm plane không
        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        
        return transform.position;
    }
    
    void UpdateAngleUI()
    {
        if (angleText != null)
        {
            // Hiển thị góc (làm tròn 1 chữ số thập phân)
            angleText.text = $"Angle: {currentAngle:F1}°";
            
            // Thêm thông tin về hướng
            string direction = GetDirectionName(currentAngle);
            angleText.text += $"\nDirection: {direction}";
        }
    }
    
    string GetDirectionName(float angle)
    {
        // Chuẩn hóa góc về khoảng 0-360
        float normalizedAngle = angle;
        if (normalizedAngle < 0) normalizedAngle += 360;
        
        // Xác định hướng dựa trên góc
        if (normalizedAngle >= 337.5f || normalizedAngle < 22.5f)
            return "North";
        else if (normalizedAngle >= 22.5f && normalizedAngle < 67.5f)
            return "North-East";
        else if (normalizedAngle >= 67.5f && normalizedAngle < 112.5f)
            return "East";
        else if (normalizedAngle >= 112.5f && normalizedAngle < 157.5f)
            return "South-East";
        else if (normalizedAngle >= 157.5f && normalizedAngle < 202.5f)
            return "South";
        else if (normalizedAngle >= 202.5f && normalizedAngle < 247.5f)
            return "South-West";
        else if (normalizedAngle >= 247.5f && normalizedAngle < 292.5f)
            return "West";
        else
            return "North-West";
    }
}
