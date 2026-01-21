using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Mini Project - UI Manager
/// Kết hợp: Lab 5 & 6 (Observer Pattern với UnityEvent)
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Health UI")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image fillImage;
    
    [Header("Health Colors")]
    [SerializeField] private Color highHealthColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;
    
    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    
    [Header("Instructions UI")]
    [SerializeField] private TextMeshProUGUI instructionsText;
    
    void Start()
    {
        // Lấy fill image từ slider
        if (fillImage == null && healthBar != null)
        {
            fillImage = healthBar.fillRect.GetComponent<Image>();
        }
        
        // Ẩn Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        // Setup restart button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        
        // Setup instructions
        if (instructionsText != null)
        {
            instructionsText.text = 
                "WASD - Move\n" +
                "Survive the Turret!";
        }
    }
    
    /// <summary>
    /// Method được bind trong Inspector với Player.onHealthChanged
    /// (Lab 6 - UnityEvent)
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
        
        // Đổi màu fill
        UpdateHealthBarColor(currentHealth / maxHealth);
    }
    
    /// <summary>
    /// Đổi màu health bar
    /// </summary>
    private void UpdateHealthBarColor(float healthPercent)
    {
        if (fillImage == null) return;
        
        if (healthPercent > 0.6f)
            fillImage.color = highHealthColor;
        else if (healthPercent > 0.3f)
            fillImage.color = mediumHealthColor;
        else
            fillImage.color = lowHealthColor;
    }
    
    /// <summary>
    /// Method được bind trong Inspector với Player.onPlayerDied
    /// (Lab 6 - UnityEvent)
    /// </summary>
    public void OnPlayerDied()
    {
        Debug.Log("[UI] Player died! Showing Game Over...");
        
        // Hiển thị UI chết
        if (healthText != null)
        {
            healthText.text = "DEAD";
            healthText.color = Color.red;
        }
        
        // Hiển thị Game Over panel sau delay
        Invoke(nameof(ShowGameOver), 1f);
    }
    
    /// <summary>
    /// Hiển thị màn hình Game Over
    /// </summary>
    private void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER\nTurret Wins!";
        }
        
        // Pause game
        Time.timeScale = 0f;
    }
    
    /// <summary>
    /// Restart game
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
