using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Lấy component AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Nhấn Space để Play
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
            Debug.Log("Audio Playing");
        }

        // Nhấn S để Stop
        if (Input.GetKeyDown(KeyCode.S))
        {
            audioSource.Stop();
            Debug.Log("Audio Stopped");
        }
    }
}
