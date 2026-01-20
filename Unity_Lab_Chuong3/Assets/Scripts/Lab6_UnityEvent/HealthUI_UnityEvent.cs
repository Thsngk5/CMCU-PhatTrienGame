using UnityEngine;

public class HealthUI_UnityEvent : MonoBehaviour
{
    public void UpdateUI(int hp)
    {
        Debug.Log("UI: HP = " + hp);
    }
}

