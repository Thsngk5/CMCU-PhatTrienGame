using UnityEngine;

public class JsonTest : MonoBehaviour
{
    void Start()
    {
        PlayerData player = new PlayerData();

        player.playerName = "Tran";
        player.level = 3;
        player.score = 150;

        string json = JsonUtility.ToJson(player);

        Debug.Log("JSON: " + json);

        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

        Debug.Log("Name: " + loadedData.playerName);
        Debug.Log("Level: " + loadedData.level);
        Debug.Log("Score: " + loadedData.score);
}
}