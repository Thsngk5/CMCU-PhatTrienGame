using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Xử lý va chạm thông thường
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision với: " + collision.gameObject.name);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Đang va chạm với: " + collision.gameObject.name);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Rời khỏi: " + collision.gameObject.name);
    }
}