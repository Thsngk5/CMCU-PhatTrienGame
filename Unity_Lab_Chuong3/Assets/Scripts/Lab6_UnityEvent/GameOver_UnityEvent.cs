using UnityEngine;

public class GameOver_UnityEvent : MonoBehaviour
{
    public void CheckGameOver(int hp)
    {
        if (hp <= 0)
        {
            Debug.Log("GAME OVER!");
        }
    }
}

