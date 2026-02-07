using UnityEngine;

public class TriggerVsCollision : MonoBehaviour
{
    // Xử lý Collision (Is Trigger = false)
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION: Va chạm với " + collision.gameObject.name);
        Debug.Log("→ Bị chặn, không đi qua được");
    }

    // Xử lý Trigger (Is Trigger = true)
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER: Đi vào " + other.gameObject.name);
        Debug.Log("→ Đi qua được, chỉ phát hiện");
    }
}
