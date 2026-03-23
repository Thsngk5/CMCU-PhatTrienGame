using UnityEngine;

public class GM : MonoBehaviour
{
    public GameConfig config;

    void Start()
    {
        Debug.Log("Player HP: " + config.playerHP);
        Debug.Log("Player Speed: " + config.playerSpeed);
        Debug.Log("Max Level: " + config.maxLevel);
    }
}