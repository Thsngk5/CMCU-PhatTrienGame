using UnityEngine;

public class AudioManagerUnityEvent : MonoBehaviour
{
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
    }
    
    /// <summary>
    /// Method này sẽ được binding trong Inspector với onHealthChanged
    /// Kiểm tra xem máu tăng hay giảm để phát âm thanh phù hợp
    /// </summary>
    public void OnHealthChanged(float currentHealth, float maxHealth)
    {
        // Lần đầu tiên, chỉ lưu giá trị
        if (lastHealth == 0)
        {
            lastHealth = currentHealth;
            return;
        }
        
        // Kiểm tra máu tăng hay giảm
        if (currentHealth < lastHealth)
        {
            // Bị damage
            PlaySound(damageSound);
            Debug.Log("[UnityEvent] Audio: Playing damage sound");
        }
        else if (currentHealth > lastHealth)
        {
            // Được heal
            PlaySound(healSound);
            Debug.Log("[UnityEvent] Audio: Playing heal sound");
        }
        
        lastHealth = currentHealth;
    }
    
    /// <summary>
    /// Method này sẽ được binding trong Inspector với onPlayerDied
    /// </summary>
    public void OnPlayerDied()
    {
        PlaySound(deathSound);
        Debug.Log("[UnityEvent] Audio: Playing death sound");
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
            Debug.LogWarning("[UnityEvent] Audio clip is not assigned!");
        }
    }
}
