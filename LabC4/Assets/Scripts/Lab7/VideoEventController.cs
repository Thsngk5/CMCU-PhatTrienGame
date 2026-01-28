using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoEventController : MonoBehaviour
{
    [Header("Video Components")]
    public VideoPlayer videoPlayer;
    
    [Header("UI Elements")]
    public GameObject endPanel;
    public Text messageText; // Hoặc TextMeshProUGUI nếu dùng TMP
    
    [Header("Settings")]
    public string nextSceneName = "GameplayScene"; // Tên scene tiếp theo
    public float delayBeforeLoadScene = 3f; // Delay 3 giây trước khi chuyển scene

    private bool hasVideoEnded = false;

    void Start()
    {
        // Kiểm tra components
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer chưa được gán!");
            return;
        }

        if (endPanel != null)
        {
            endPanel.SetActive(false); // Ẩn panel lúc đầu
        }

        // Đăng ký event listeners
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.loopPointReached += OnVideoEnded;
        videoPlayer.errorReceived += OnVideoError;

        // Bắt đầu chuẩn bị video
        Debug.Log("Đang chuẩn bị video...");
        videoPlayer.Prepare();
    }

    void Update()
    {
        // Nhấn V để play video (nếu chưa play)
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!videoPlayer.isPlaying && !hasVideoEnded)
            {
                PlayVideo();
            }
        }

        // Nhấn R để reset (replay)
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetVideo();
        }

        // Debug info
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowVideoInfo();
        }
    }

    // Event: Video đã được chuẩn bị xong
    void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("✓ Video đã sẵn sàng!");
        Debug.Log($"Video duration: {vp.length} giây");
        Debug.Log($"Frame count: {vp.frameCount}");
        
        // Tự động play (hoặc đợi nhấn V)
        // vp.Play(); // Uncomment dòng này nếu muốn tự động play
    }

    // Event: Video đã phát đến cuối
    void OnVideoEnded(VideoPlayer vp)
    {
        Debug.Log("✓ Video đã kết thúc!");
        hasVideoEnded = true;

        // Hiển thị EndPanel
        ShowEndPanel();

        // Chuyển scene sau delay
        Invoke("LoadNextScene", delayBeforeLoadScene);
    }

    // Event: Lỗi khi phát video
    void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.LogError($"✗ Lỗi video: {message}");
    }

    // Phát video
    void PlayVideo()
    {
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();
            Debug.Log("▶ Đang phát video...");
        }
        else
        {
            Debug.LogWarning("Video chưa sẵn sàng!");
        }
    }

    // Hiển thị panel kết thúc
    void ShowEndPanel()
    {
        if (endPanel != null)
        {
            endPanel.SetActive(true);
            
            if (messageText != null)
            {
                messageText.text = "Video đã kết thúc!\nChuyển scene sau " + delayBeforeLoadScene + " giây...";
            }
        }
    }

    // Reset video để xem lại
    void ResetVideo()
    {
        Debug.Log("↻ Reset video");
        
        hasVideoEnded = false;
        
        if (endPanel != null)
            endPanel.SetActive(false);

        videoPlayer.Stop();
        videoPlayer.time = 0;
        videoPlayer.Play();
    }

    // Chuyển scene
    void LoadNextScene()
    {
        Debug.Log($"→ Chuyển sang scene: {nextSceneName}");
        
        // Kiểm tra scene có tồn tại không
        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning($"Scene '{nextSceneName}' không tồn tại! Vui lòng thêm vào Build Settings.");
        }
    }

    // Hiển thị thông tin video (debug)
    void ShowVideoInfo()
    {
        if (videoPlayer != null)
        {
            Debug.Log("=== VIDEO INFO ===");
            Debug.Log($"Is Playing: {videoPlayer.isPlaying}");
            Debug.Log($"Is Prepared: {videoPlayer.isPrepared}");
            Debug.Log($"Current Time: {videoPlayer.time:F2}s / {videoPlayer.length:F2}s");
            Debug.Log($"Frame: {videoPlayer.frame} / {videoPlayer.frameCount}");
        }
    }

    // Cleanup khi destroy object
    void OnDestroy()
    {
        // Hủy đăng ký events để tránh memory leak
        if (videoPlayer != null)
        {
            videoPlayer.prepareCompleted -= OnVideoPrepared;
            videoPlayer.loopPointReached -= OnVideoEnded;
            videoPlayer.errorReceived -= OnVideoError;
        }
    }
}