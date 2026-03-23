using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    string path;

    void Start()
    {
        path = Application.persistentDataPath + "/savegame.json";

        SaveGame();
        LoadGame();
        Debug.Log(Application.persistentDataPath);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        data.level = 2;
        data.score = 250;
        data.timePlayed = 120.5f;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(path, json);

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Level: " + data.level);
            Debug.Log("Score: " + data.score);
            Debug.Log("Time Played: " + data.timePlayed);
        }
    }
}