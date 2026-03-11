using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject optionsPanel;

    public void StartGame()
    {
        Debug.Log("Game Started");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        Debug.Log("Options Opened");
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }
}
