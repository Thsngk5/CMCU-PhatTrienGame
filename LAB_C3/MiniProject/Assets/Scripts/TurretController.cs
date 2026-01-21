using UnityEngine;

/// <summary>
/// Mini Project - Turret Controller
/// Kết hợp: Lab 1 (Lifecycle), Lab 3 (Quaternion Rotation), Lab 4 (Signed Angle)
/// </summary>
public class TurretController : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target; // Player
    [SerializeField] private Transform turretHead; // Phần xoay của turret
    
    [Header("Rotation Settings")]
    [SerializeField] private bool useSmoothRotation = true;
    [SerializeField] private float rotationSpeed = 5f; // Cho Slerp/RotateTowards
    
    [Header("Combat Settings")]
    [SerializeField] private float fireRate = 1f; // Bắn mỗi 1 giây
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 15f;
    
    [Header("Visual Debug")]
    [SerializeField] private bool showAngleUI = true;
    
    // Private
    private float nextFireTime;
    private float currentAngle; // Lab 4 - Hiển thị góc
    private bool enableDebugLog = true;
    
    #region Lifecycle (Lab 1)
    void Awake()
    {
        if (enableDebugLog) Debug.Log($"[Turret] Awake - {Time.time}");
        
        // Tự động tìm turretHead nếu chưa gán
        if (turretHead == null)
            turretHead = transform;
    }
    
    void OnEnable()
    {
        if (enableDebugLog) Debug.Log($"[Turret] OnEnable - {Time.time}");
    }
    
    void Start()
    {
        if (enableDebugLog) Debug.Log($"[Turret] Start - {Time.time}");
        
        // Tự động tìm Player
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
        
        nextFireTime = Time.time + 1f / fireRate;
    }
    
    void Update()
    {
        if (target == null) return;
        
        RotateTowardsTarget();
        TryShoot();
    }
    
    void OnDisable()
    {
        if (enableDebugLog) Debug.Log($"[Turret] OnDisable - {Time.time}");
    }
    
    void OnDestroy()
    {
        if (enableDebugLog) Debug.Log($"[Turret] OnDestroy - {Time.time}");
    }
    #endregion
    
    #region Rotation (Lab 3 + Lab 4)
    /// <summary>
    /// Xoay turret nhìn về Player
    /// Lab 3: LookAt vs Slerp/RotateTowards
    /// Lab 4: Tính Signed Angle
    /// </summary>
    private void RotateTowardsTarget()
    {
        // Tính hướng đến target
        Vector3 direction = target.position - turretHead.position;
        direction.y = 0; // Chỉ xoay trục Y (topdown)
        
        if (direction == Vector3.zero) return;
        
        // Tính góc (Lab 4 - Signed Angle)
        currentAngle = Vector3.SignedAngle(turretHead.forward, direction, Vector3.up);
        
        // Xoay (Lab 3)
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        if (useSmoothRotation)
        {
            // Xoay mượt bằng Slerp
            turretHead.rotation = Quaternion.Slerp(
                turretHead.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            // Xoay trực tiếp
            turretHead.rotation = targetRotation;
        }
    }
    #endregion
    
    #region Combat
    /// <summary>
    /// Bắn vào Player
    /// </summary>
    private void TryShoot()
    {
        if (Time.time < nextFireTime) return;
        
        // Kiểm tra khoảng cách
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > range) return;
        
        // Kiểm tra góc (chỉ bắn khi nhắm gần chính xác)
        if (Mathf.Abs(currentAngle) > 5f) return;
        
        // Bắn
        Shoot();
        nextFireTime = Time.time + 1f / fireRate;
    }
    
    /// <summary>
    /// Gây damage cho Player
    /// </summary>
    private void Shoot()
    {
        Debug.Log("[Turret] FIRE!");
        
        // Tìm PlayerController và gây damage
        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        
        // TODO: Có thể thêm hiệu ứng bắn, particle, sound...
    }
    #endregion
    
    #region Gizmos & Debug
    void OnDrawGizmos()
    {
        if (turretHead == null) return;
        
        // Vẽ hướng turret đang nhìn (màu đỏ)
        Gizmos.color = Color.red;
        Gizmos.DrawRay(turretHead.position, turretHead.forward * 3f);
        
        // Vẽ tầm bắn
        Gizmos.color = Color.yellow;
        DrawWireCircle(transform.position, range);
        
        // Vẽ line đến target
        if (target != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(turretHead.position, target.position);
        }
    }
    
    private void DrawWireCircle(Vector3 center, float radius)
    {
        int segments = 32;
        float angle = 0f;
        Vector3 lastPoint = center + new Vector3(radius, 0, 0);
        
        for (int i = 1; i <= segments; i++)
        {
            angle = i * 360f / segments * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Gizmos.DrawLine(lastPoint, newPoint);
            lastPoint = newPoint;
        }
    }
    
    void OnGUI()
    {
        if (!showAngleUI) return;
        
        // Hiển thị góc xoay (Lab 4)
        GUI.Label(new Rect(10, 10, 300, 20), 
            $"Turret Angle: {currentAngle:F1}°");
        GUI.Label(new Rect(10, 30, 300, 20), 
            $"Rotation Mode: {(useSmoothRotation ? "Smooth (Slerp)" : "Instant (LookAt)")}");
        
        // Hiển thị khoảng cách
        if (target != null)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            GUI.Label(new Rect(10, 50, 300, 20), 
                $"Distance: {dist:F1}m / {range}m");
        }
    }
    #endregion
}
