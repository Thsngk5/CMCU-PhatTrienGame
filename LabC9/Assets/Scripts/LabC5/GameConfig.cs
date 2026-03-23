using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int playerHP;
    public float playerSpeed;
    public int maxLevel;
}