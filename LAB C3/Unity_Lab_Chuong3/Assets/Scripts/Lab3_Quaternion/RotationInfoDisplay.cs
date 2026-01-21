using UnityEngine;
using UnityEngine.UI;

public class RotationInfoDisplay : MonoBehaviour
{
    [SerializeField] private TurretController turretController;
    [SerializeField] private Text infoText;

    void Update()
    {
        if (infoText == null) return;

        infoText.text = @"=== TURRET ROTATION CONTROLS ===

        [1] LookAt - Xoay tức thì
        [2] RotateTowards - Xoay với tốc độ cố định  
        [3] Slerp - Xoay mượt mà

        === TARGET CONTROLS ===
        [C] Circle movement
        [R] Random movement
        [M] Manual (Arrow keys)

        === TIP ===
        Di chuyển target nhanh để thấy 
        sự khác biệt giữa các chế độ!";
    }
}
