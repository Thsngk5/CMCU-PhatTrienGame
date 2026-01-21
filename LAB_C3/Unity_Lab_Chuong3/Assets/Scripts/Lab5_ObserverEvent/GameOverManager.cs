using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private PlayerHealth playerHealth;
    
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    
    [Header("Settings")]
    [SerializeField] private float gameOverDelay = 1f; // Delay trước khi hiện panel
    
    void Start()
    {
        // Tìm PlayerHealth
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }
        
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth not found!");
            return;
        }
        
        // Ẩn panel game over lúc đầu
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        // Setup restart button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        
        // SUBSCRIBE vào event chết
        playerHealth.OnPlayerDied += OnPlayerDied;
    }
    
    void OnDestroy()
    {
        // UNSUBSCRIBE
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDied -= OnPlayerDied;
        }
    }
    
    /// <summary>
    /// Observer method - xử lý khi player chết
    /// </summary>
    private void OnPlayerDied()
    {
        Debug.Log("GameOver: Player died, showing game over screen...");
        
        // Hiển thị game over sau delay
        Invoke(nameof(ShowGameOver), gameOverDelay);
        
        // Có thể pause game
        // Time.timeScale = 0f;
    }
    
    /// <summary>
    /// Hiển thị màn hình game over
    /// </summary>
    private void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER";
        }
    }
    
    /// <summary>
    /// Restart game
    /// </summary>
    public void RestartGame()
    {
        // Reset time scale nếu đã pause
        Time.timeScale = 1f;
        
        // Reload scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
