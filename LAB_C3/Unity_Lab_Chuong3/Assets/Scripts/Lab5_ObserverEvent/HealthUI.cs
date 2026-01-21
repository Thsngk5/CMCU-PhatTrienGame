using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    
    [Header("Player Reference")]
    [SerializeField] private PlayerHealth playerHealth;
    
    [Header("Visual Effects")]
    [SerializeField] private Image fillImage; // Fill của slider
    [SerializeField] private Color highHealthColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;
    
    void Start()
    {
        // Tìm PlayerHealth nếu chưa gán
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }
        
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth not found!");
            return;
        }
        
        // Lấy fill image từ slider
        if (fillImage == null && healthBar != null)
        {
            fillImage = healthBar.fillRect.GetComponent<Image>();
        }
        
        // SUBSCRIBE vào events của PlayerHealth
        playerHealth.OnHealthChanged += UpdateHealthUI;
        playerHealth.OnPlayerDied += OnPlayerDied;
        
        // Cập nhật UI lần đầu
        UpdateHealthUI(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }
    
    void OnDestroy()
    {
        // UNSUBSCRIBE khi object bị destroy (tránh memory leak)
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthUI;
            playerHealth.OnPlayerDied -= OnPlayerDied;
        }
    }
    
    /// <summary>
    /// Observer method - được gọi khi health thay đổi
    /// </summary>
    private void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        // Cập nhật slider
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        
        // Cập nhật text
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth:F0} / {maxHealth:F0}";
        }
        
        // Đổi màu fill theo % máu
        UpdateHealthBarColor(currentHealth / maxHealth);
    }
    
    /// <summary>
    /// Đổi màu health bar theo % máu còn lại
    /// </summary>
    private void UpdateHealthBarColor(float healthPercent)
    {
        if (fillImage == null) return;
        
        if (healthPercent > 0.6f)
        {
            fillImage.color = highHealthColor;
        }
        else if (healthPercent > 0.3f)
        {
            fillImage.color = mediumHealthColor;
        }
        else
        {
            fillImage.color = lowHealthColor;
        }
    }
    
    /// <summary>
    /// Observer method - được gọi khi player chết
    /// </summary>
    private void OnPlayerDied()
    {
        Debug.Log("UI: Player died, updating display...");
        
        // Có thể thêm hiệu ứng UI khi chết
        if (healthText != null)
        {
            healthText.text = "DEAD";
            healthText.color = Color.red;
        }
    }
}