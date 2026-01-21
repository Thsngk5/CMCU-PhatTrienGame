using UnityEngine;

/// <summary>
/// Mini Project - Audio Manager
/// Kết hợp: Lab 5 & 6 (Observer Pattern)
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip playerHitSound;
    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip turretFireSound;
    
    [Header("Audio Settings")]
    [SerializeField] private float volume = 0.5f;
    
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
    }
    
    /// <summary>
    /// Method được bind trong Inspector với Player.onHealthChanged
    /// (Lab 6 - UnityEvent)
    /// </summary>
    public void OnHealthChanged(float currentHealth, float maxHealth)
    {
        // Lần đầu tiên chỉ lưu giá trị
        if (lastHealth == 0)
        {
            lastHealth = currentHealth;
            return;
        }
        
        // Nếu máu giảm → phát âm bị đánh
        if (currentHealth < lastHealth)
        {
            PlaySound(playerHitSound);
            Debug.Log("[Audio] Player hit!");
        }
        
        lastHealth = currentHealth;
    }
    
    /// <summary>
    /// Method được bind trong Inspector với Player.onPlayerDied
    /// (Lab 6 - UnityEvent)
    /// </summary>
    public void OnPlayerDied()
    {
        PlaySound(playerDeathSound);
        Debug.Log("[Audio] Player death sound!");
    }
    
    /// <summary>
    /// Public method để Turret gọi khi bắn
    /// </summary>
    public void PlayTurretFireSound()
    {
        PlaySound(turretFireSound);
    }
    
    /// <summary>
    /// Helper method
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}