using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Selected Level: " + DataHolder.selectedLevel);
    }
}