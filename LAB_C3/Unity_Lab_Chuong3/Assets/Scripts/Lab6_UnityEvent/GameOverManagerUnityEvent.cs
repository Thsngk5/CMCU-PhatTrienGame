using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManagerUnityEvent : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    
    [Header("Settings")]
    [SerializeField] private float gameOverDelay = 1f;
    
    void Start()
    {
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
    }
    
    /// <summary>
    /// Method này sẽ được binding trong Inspector với onPlayerDied
    /// KHÔNG CẦN subscribe/unsubscribe
    /// </summary>
    public void OnPlayerDied()
    {
        Debug.Log("[UnityEvent] GameOver: Player died, showing game over screen...");
        
        // Hiển thị game over sau delay
        Invoke(nameof(ShowGameOver), gameOverDelay);
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
