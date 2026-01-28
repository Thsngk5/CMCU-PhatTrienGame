using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroCutsceneManager : MonoBehaviour
{
    [Header("=== VIDEO & AUDIO ===")]
    [Tooltip("Component VideoPlayer để phát intro video")]
    public VideoPlayer videoPlayer;
    
    [Tooltip("AudioSource phát nhạc nền intro")]
    public AudioSource bgmAudioSource;

    [Header("=== UI ELEMENTS ===")]
    [Tooltip("Button để skip intro")]
    public Button skipButton;
    
    [Tooltip("Panel màu đen dùng để fade in/out")]
    public CanvasGroup fadePanel;

    [Header("=== SETTINGS ===")]
    [Tooltip("Tên scene gameplay sẽ load sau intro")]
    public string gameplaySceneName = "GameplayScene";
    
    [Tooltip("Tốc độ fade (càng lớn càng nhanh)")]
    [Range(0.1f, 5f)]
    public float fadeSpeed = 1.5f;
    
    [Tooltip("Fade in khi bắt đầu intro")]
    public bool fadeInOnStart = true;
    
    [Tooltip("Thời gian fade in (giây)")]
    public float fadeInDuration = 1f;

    // Biến internal
    private bool isSkipping = false;
    private bool hasVideoStarted = false;

    void Start()
    {
        Debug.Log("=== INTRO CUTSCENE START ===");
        
        // Validate components
        ValidateComponents();

        // Setup video events
        if (videoPlayer != null)
        {
            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.loopPointReached += OnVideoCompleted;
            videoPlayer.errorReceived += OnVideoError;
            
            Debug.Log("Đang chuẩn bị video...");
            videoPlayer.Prepare();
        }

        // Setup skip button
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(OnSkipButtonClicked);
        }

        // Fade in nếu cần
        if (fadeInOnStart && fadePanel != null)
        {
            StartCoroutine(FadeIn());
        }
    }

    // Kiểm tra components có được gán đủ không
    void ValidateComponents()
    {
        bool isValid = true;

        if (videoPlayer == null)
        {
            Debug.LogError("❌ VideoPlayer chưa được gán!");
            isValid = false;
        }

        if (skipButton == null)
        {
            Debug.LogWarning("⚠ Skip Button chưa được gán!");
        }

        if (fadePanel == null)
        {
            Debug.LogWarning("⚠ Fade Panel chưa được gán!");
        }

        if (isValid)
        {
            Debug.Log("✓ Tất cả components đã sẵn sàng");
        }
    }

    // === VIDEO EVENTS ===

    void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log($"✓ Video đã sẵn sàng! Duration: {vp.length:F2}s");
        
        // Play video
        vp.Play();
        hasVideoStarted = true;
        
        // Play BGM
        if (bgmAudioSource != null && !bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Play();
            Debug.Log("♪ BGM bắt đầu phát");
        }
    }

    void OnVideoCompleted(VideoPlayer vp)
    {
        if (isSkipping) return; // Đã skip rồi thì không cần xử lý
        
        Debug.Log("✓ Video đã kết thúc tự nhiên");
        LoadGameplay();
    }

    void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.LogError($"❌ Lỗi video: {message}");
        
        // Nếu video lỗi, vẫn cho phép skip
        if (skipButton != null)
        {
            skipButton.interactable = true;
        }
    }

    // === SKIP LOGIC ===

    public void OnSkipButtonClicked()
    {
        if (isSkipping)
        {
            Debug.Log("Đang trong quá trình skip...");
            return;
        }

        isSkipping = true;
        Debug.Log("⏭ Người chơi nhấn Skip");

        // Stop video và audio
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        if (bgmAudioSource != null && bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }

        // Load gameplay
        LoadGameplay();
    }

    // === SCENE TRANSITION ===

    void LoadGameplay()
    {
        Debug.Log($"→ Chuyển sang scene: {gameplaySceneName}");
        
        // Disable skip button
        if (skipButton != null)
        {
            skipButton.interactable = false;
        }

        // Fade out và load scene
        StartCoroutine(FadeOutAndLoad());
    }

    // === FADE EFFECTS ===

    IEnumerator FadeIn()
    {
        if (fadePanel == null) yield break;

        Debug.Log("▼ Fade in...");
        fadePanel.alpha = 1f;

        float elapsed = 0f;
        
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeInDuration);
            yield return null;
        }

        fadePanel.alpha = 0f;
        Debug.Log("✓ Fade in hoàn tất");
    }

    IEnumerator FadeOutAndLoad()
    {
        if (fadePanel == null)
        {
            // Không có fade panel, load trực tiếp
            LoadSceneDirectly();
            yield break;
        }

        Debug.Log("▲ Fade out...");
        
        float alpha = fadePanel.alpha;
        float targetAlpha = 1f;

        while (alpha < targetAlpha)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadePanel.alpha = Mathf.Clamp01(alpha);
            yield return null;
        }

        fadePanel.alpha = 1f;
        Debug.Log("✓ Fade out hoàn tất");

        // Đợi 1 frame
        yield return null;

        // Load scene
        LoadSceneDirectly();
    }

    void LoadSceneDirectly()
    {
        if (string.IsNullOrEmpty(gameplaySceneName))
        {
            Debug.LogError("❌ Gameplay Scene Name chưa được set!");
            return;
        }

        // Kiểm tra scene có trong Build Settings không
        if (Application.CanStreamedLevelBeLoaded(gameplaySceneName))
        {
            SceneManager.LoadScene(gameplaySceneName);
        }
        else
        {
            Debug.LogError($"❌ Scene '{gameplaySceneName}' không có trong Build Settings!");
            Debug.Log("→ Hãy thêm scene vào File > Build Settings > Add Open Scenes");
        }
    }

    // === DEBUG CONTROLS ===

    void Update()
    {
        // Nhấn ESC hoặc Space cũng skip được (optional)
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if (hasVideoStarted && !isSkipping)
            {
                OnSkipButtonClicked();
            }
        }

        // Debug info - nhấn I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowDebugInfo();
        }
    }

    void ShowDebugInfo()
    {
        Debug.Log("=== INTRO DEBUG INFO ===");
        
        if (videoPlayer != null)
        {
            Debug.Log($"Video Playing: {videoPlayer.isPlaying}");
            Debug.Log($"Video Time: {videoPlayer.time:F2} / {videoPlayer.length:F2}s");
        }
        
        if (bgmAudioSource != null)
        {
            Debug.Log($"BGM Playing: {bgmAudioSource.isPlaying}");
            Debug.Log($"BGM Time: {bgmAudioSource.time:F2}s");
        }
        
        Debug.Log($"Is Skipping: {isSkipping}");
    }

    // === CLEANUP ===

    void OnDestroy()
    {
        // Unregister events
        if (videoPlayer != null)
        {
            videoPlayer.prepareCompleted -= OnVideoPrepared;
            videoPlayer.loopPointReached -= OnVideoCompleted;
            videoPlayer.errorReceived -= OnVideoError;
        }

        if (skipButton != null)
        {
            skipButton.onClick.RemoveListener(OnSkipButtonClicked);
        }

        Debug.Log("=== INTRO CUTSCENE CLEANUP ===");
    }
}
