using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        // Nhấn V để play
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
                Debug.Log("Video Playing");
            }
        }
    }
}
