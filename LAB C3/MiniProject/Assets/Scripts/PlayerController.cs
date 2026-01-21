using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mini Project - Player Controller
/// Kết hợp: Lab 1 (Lifecycle), Lab 2 (Movement + Gizmos), Lab 5 (Health Event)
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    
    [Header("Unity Events")]
    public UnityEvent<float, float> onHealthChanged;
    public UnityEvent onPlayerDied;
    
    // Properties
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;
    
    // Private
    private Vector3 moveDirection;
    private bool enableDebugLog = true; // Toggle để bật/tắt log
    
    #region Lifecycle (Lab 1)
    void Awake()
    {
        if (enableDebugLog) Debug.Log($"[Player] Awake - {Time.time}");
        
        // Khởi tạo Events
        if (onHealthChanged == null)
            onHealthChanged = new UnityEvent<float, float>();
        if (onPlayerDied == null)
            onPlayerDied = new UnityEvent();
    }
    
    void OnEnable()
    {
        if (enableDebugLog) Debug.Log($"[Player] OnEnable - {Time.time}");
    }
    
    void Start()
    {
        if (enableDebugLog) Debug.Log($"[Player] Start - {Time.time}");
        
        // Khởi tạo máu
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    void Update()
    {
        if (IsDead) return;
        HandleMovementInput();
    }
    
    void FixedUpdate()
    {
        if (IsDead) return;
        ApplyMovement();
    }
    
    void OnDisable()
    {
        if (enableDebugLog) Debug.Log($"[Player] OnDisable - {Time.time}");
    }
    
    void OnDestroy()
    {
        if (enableDebugLog) Debug.Log($"[Player] OnDestroy - {Time.time}");
    }
    #endregion
    
    #region Movement (Lab 2)
    /// <summary>
    /// Xử lý input WASD
    /// </summary>
    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D
        float vertical = Input.GetAxisRaw("Vertical");     // W/S
        
        // Tạo vector di chuyển
        moveDirection = new Vector3(horizontal, 0, vertical);
        
        // NORMALIZE để tránh chạy chéo nhanh hơn (Lab 2)
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }
    }
    
    /// <summary>
    /// Apply movement trong FixedUpdate
    /// </summary>
    private void ApplyMovement()
    {
        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * moveSpeed * Time.fixedDeltaTime;
        }
    }
    
    /// <summary>
    /// Vẽ Gizmos hiển thị hướng di chuyển (Lab 2)
    /// </summary>
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        
        // Vẽ hướng di chuyển màu xanh lá
        if (moveDirection != Vector3.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, moveDirection * 2f);
        }
        
        // Vẽ vòng tròn tầm nhìn
        Gizmos.color = Color.cyan;
        DrawWireCircle(transform.position, 1f);
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
    #endregion
    
    #region Health System (Lab 5)
    /// <summary>
    /// Nhận sát thương từ Turret
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        Debug.Log($"[Player] Took {damage} damage! HP: {currentHealth}/{maxHealth}");
        
        // Phát event
        onHealthChanged?.Invoke(currentHealth, maxHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    /// <summary>
    /// Hồi máu
    /// </summary>
    public void Heal(float amount)
    {
        if (IsDead) return;
        
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    /// <summary>
    /// Xử lý khi chết
    /// </summary>
    private void Die()
    {
        Debug.Log("[Player] DIED!");
        onPlayerDied?.Invoke();
        
        // Disable movement
        moveSpeed = 0;
    }
    #endregion
}
