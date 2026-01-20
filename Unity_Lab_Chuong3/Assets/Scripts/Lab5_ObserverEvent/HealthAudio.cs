using UnityEngine;

public class HealthAudio : MonoBehaviour
{
    public PlayerHealth player;

    void OnEnable()
    {
        player.OnHealthChanged += PlaySound;
    }

    void OnDisable()
    {
        player.OnHealthChanged -= PlaySound;
    }

    void PlaySound(int hp)
    {
        Debug.Log("Audio: HP changed sound!");
    }
}

