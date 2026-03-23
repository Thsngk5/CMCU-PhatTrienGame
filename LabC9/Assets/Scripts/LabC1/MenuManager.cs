using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartLevel(int level)
    {
        DataHolder.selectedLevel = level;
        SceneManager.LoadScene("Game");
    }
}