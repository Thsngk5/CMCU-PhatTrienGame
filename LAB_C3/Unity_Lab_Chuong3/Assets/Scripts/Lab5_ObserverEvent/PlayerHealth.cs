using UnityEngine;
using System; // Cần cho Action/Event

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    
    [Header("Damage Test")]
    [SerializeField] private float damageAmount = 10f;
    
    // Định nghĩa Events
    // Event được gọi khi máu thay đổi (truyền current và max health)
    public event Action<float, float> OnHealthChanged;
    
    // Event được gọi khi player chết
    public event Action OnPlayerDied;
    
    // Property để các class khác đọc health (không cho set trực tiếp)
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;
    
    void Start()
    {
        // Khởi tạo máu đầy
        currentHealth = maxHealth;
        
        // Phát event lần đầu để UI cập nhật
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    void Update()
    {
        // Nhấn H để test trừ máu
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(damageAmount);
        }
        
        // Nhấn R để hồi máu (test)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Heal(20f);
        }
    }
    
    /// <summary>
    /// Nhận sát thương
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (IsDead) return; // Đã chết thì không nhận damage nữa
        
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Không cho âm
        
        Debug.Log($"Player took {damage} damage. Current HP: {currentHealth}/{maxHealth}");
        
        // Phát event báo máu thay đổi
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
        // Nếu chết, phát event chết
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
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Không vượt max
        
        Debug.Log($"Player healed {amount}. Current HP: {currentHealth}/{maxHealth}");
        
        // Phát event
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    /// <summary>
    /// Xử lý khi player chết
    /// </summary>
    private void Die()
    {
        Debug.Log("Player Died!");
        
        // Phát event chết
        OnPlayerDied?.Invoke();
    }
    
    /// <summary>
    /// Reset health (để test lại)
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
