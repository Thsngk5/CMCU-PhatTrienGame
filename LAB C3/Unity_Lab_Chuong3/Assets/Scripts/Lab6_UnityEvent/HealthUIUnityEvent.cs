using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUIUnityEvent : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    
    [Header("Visual Effects")]
    [SerializeField] private Image fillImage;
    [SerializeField] private Color highHealthColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;
    
    void Start()
    {
        // Lấy fill image từ slider
        if (fillImage == null && healthBar != null)
        {
            fillImage = healthBar.fillRect.GetComponent<Image>();
        }
    }
    
    /// <summary>
    /// Method này sẽ được binding trong Inspector với onHealthChanged
    /// KHÔNG CẦN subscribe/unsubscribe trong code
    /// </summary>
    public void UpdateHealthUI(float currentHealth, float maxHealth)
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
        
        Debug.Log($"[UnityEvent] UI Updated: {currentHealth}/{maxHealth}");
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
    /// Method này sẽ được binding trong Inspector với onPlayerDied
    /// </summary>
    public void OnPlayerDied()
    {
        Debug.Log("[UnityEvent] UI: Player died!");
        
        if (healthText != null)
        {
            healthText.text = "DEAD";
            healthText.color = Color.red;
        }
    }
}
