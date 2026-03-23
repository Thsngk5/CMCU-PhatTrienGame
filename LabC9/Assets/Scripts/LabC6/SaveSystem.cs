using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    string path;

    void Start()
    {
        path = Application.persistentDataPath + "/save.txt";

        SaveGame();
        LoadGame();
        Debug.Log(Application.persistentDataPath);
    }

    void SaveGame()
    {
        string data = "Level:3 Score:150";

        File.WriteAllText(path, data);

        Debug.Log("Game Saved");
    }

    void LoadGame()
    {
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);

            Debug.Log("Loaded Data: " + data);
        }
    }
}