using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("=== ĐANG Ở GAMEPLAY SCENE ===");
    }

    void Update()
    {
        // Nhấn R để quay lại intro
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("IntroScene");
        }
    }
}
