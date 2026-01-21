using UnityEngine;
using UnityEngine.Events;

// Custom UnityEvent với 2 tham số (currentHealth, maxHealth)
[System.Serializable]
public class HealthChangedEvent : UnityEvent<float, float> { }

public class PlayerHealthUnityEvent : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    
    [Header("Damage Test")]
    [SerializeField] private float damageAmount = 10f;
    
    [Header("Unity Events")]
    // UnityEvent được hiển thị trong Inspector
    public HealthChangedEvent onHealthChanged;
    public UnityEvent onPlayerDied;
    
    // Property để các class khác đọc health
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;
    
    void Awake()
    {
        // Khởi tạo UnityEvent nếu null
        if (onHealthChanged == null)
            onHealthChanged = new HealthChangedEvent();
        
        if (onPlayerDied == null)
            onPlayerDied = new UnityEvent();
    }
    
    void Start()
    {
        // Khởi tạo máu đầy
        currentHealth = maxHealth;
        
        // Invoke UnityEvent lần đầu
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    void Update()
    {
        // Nhấn J để test trừ máu (thay H để tránh trùng Lab 5)
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(damageAmount);
        }
        
        // Nhấn K để hồi máu (thay R để tránh trùng Lab 5)
        if (Input.GetKeyDown(KeyCode.K))
        {
            Heal(20f);
        }
    }
    
    /// <summary>
    /// Nhận sát thương
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        Debug.Log($"[UnityEvent] Player took {damage} damage. Current HP: {currentHealth}/{maxHealth}");
        
        // Invoke UnityEvent
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
        
        Debug.Log($"[UnityEvent] Player healed {amount}. Current HP: {currentHealth}/{maxHealth}");
        
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    /// <summary>
    /// Xử lý khi player chết
    /// </summary>
    private void Die()
    {
        Debug.Log("[UnityEvent] Player Died!");
        
        // Invoke UnityEvent
        onPlayerDied?.Invoke();
    }
    
    /// <summary>
    /// Reset health (để test lại)
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
