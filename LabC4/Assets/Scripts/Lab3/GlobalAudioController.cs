using UnityEngine;

public class GlobalAudioController : MonoBehaviour
{
    private bool isMuted = false;
    private bool isPaused = false;

    void Update()
    {
        // Phím M: Mute/Unmute
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMuted = !isMuted;
            AudioListener.volume = isMuted ? 0f : 1f;
            Debug.Log(isMuted ? "Audio Muted" : "Audio Unmuted");
        }

        // Phím P: Pause/Resume
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            AudioListener.pause = isPaused;
            Debug.Log(isPaused ? "Audio Paused" : "Audio Resumed");
        }
    }
}
