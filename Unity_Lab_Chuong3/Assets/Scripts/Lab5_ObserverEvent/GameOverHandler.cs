using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    public PlayerHealth player;

    void OnEnable()
    {
        player.OnHealthChanged += CheckGameOver;
    }

    void OnDisable()
    {
        player.OnHealthChanged -= CheckGameOver;
    }

    void CheckGameOver(int hp)
    {
        if (hp <= 0)
        {
            Debug.Log("GAME OVER!");
        }
    }
}

