using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth player;

    void OnEnable()
    {
        player.OnHealthChanged += UpdateUI;
    }

    void OnDisable()
    {
        player.OnHealthChanged -= UpdateUI;
    }

    void UpdateUI(int hp)
    {
        Debug.Log("UI: Current HP = " + hp);
    }
}

