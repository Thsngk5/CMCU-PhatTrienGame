using UnityEngine;

public class MovementComparison : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform normalizedPlayer;  // Player dùng normalized
    [SerializeField] private Transform rawPlayer;         // Player KHÔNG dùng normalized
    
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        // Player 1: Dùng Normalized (đúng)
        if (normalizedPlayer != null)
        {
            Vector3 normalized = moveDirection.normalized;
            normalizedPlayer.Translate(normalized * moveSpeed * Time.deltaTime, Space.World);
        }

        // Player 2: KHÔNG dùng Normalized (sai - chạy chéo nhanh hơn)
        if (rawPlayer != null)
        {
            rawPlayer.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnDrawGizmos()
    {
        if (normalizedPlayer != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(normalizedPlayer.position, 0.5f);
            Gizmos.DrawLine(normalizedPlayer.position, normalizedPlayer.position + Vector3.up * 2);
        }

        if (rawPlayer != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(rawPlayer.position, 0.5f);
            Gizmos.DrawLine(rawPlayer.position, rawPlayer.position + Vector3.up * 2);
        }
    }
}
