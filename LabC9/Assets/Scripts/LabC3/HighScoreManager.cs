using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public int score = 0;

    void Start()
    {
        score = Random.Range(0, 100);

        Debug.Log("Score: " + score);

        SaveHighScore();
        LoadHighScore();
    }

    public void SaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void LoadHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        Debug.Log("HighScore: " + highScore);
    }
}