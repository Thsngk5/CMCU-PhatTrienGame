using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private PlayerHealth playerHealth;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip deathSound;
    
    [Header("Audio Settings")]
    [SerializeField] private float volume = 1f;
    
    private AudioSource audioSource;
    private float lastHealth;
    
    void Start()
    {
        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = volume;
        
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
        
        // Lưu máu ban đầu
        lastHealth = playerHealth.CurrentHealth;
        
        // SUBSCRIBE vào events
        playerHealth.OnHealthChanged += OnHealthChanged;
        playerHealth.OnPlayerDied += OnPlayerDied;
    }
    
    void OnDestroy()
    {
        // UNSUBSCRIBE
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= OnHealthChanged;
            playerHealth.OnPlayerDied -= OnPlayerDied;
        }
    }
    
    /// <summary>
    /// Observer method - phát âm thanh khi máu thay đổi
    /// </summary>
    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        // Kiểm tra máu tăng hay giảm
        if (currentHealth < lastHealth)
        {
            // Bị damage
            PlaySound(damageSound);
            Debug.Log("Audio: Playing damage sound");
        }
        else if (currentHealth > lastHealth)
        {
            // Được heal
            PlaySound(healSound);
            Debug.Log("Audio: Playing heal sound");
        }
        
        lastHealth = currentHealth;
    }
    
    /// <summary>
    /// Observer method - phát âm thanh chết
    /// </summary>
    private void OnPlayerDied()
    {
        PlaySound(deathSound);
        Debug.Log("Audio: Playing death sound");
    }
    
    /// <summary>
    /// Helper method phát âm thanh
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else if (clip == null)
        {
            Debug.LogWarning("Audio clip is not assigned!");
        }
    }
}
