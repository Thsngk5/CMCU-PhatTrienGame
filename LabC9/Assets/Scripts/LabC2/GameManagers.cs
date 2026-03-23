using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers instance;

    public int score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}