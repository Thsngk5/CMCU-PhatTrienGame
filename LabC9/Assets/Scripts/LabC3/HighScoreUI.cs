using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    public Text highScoreText;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        highScoreText.text = "High Score: " + highScore;
    }
}