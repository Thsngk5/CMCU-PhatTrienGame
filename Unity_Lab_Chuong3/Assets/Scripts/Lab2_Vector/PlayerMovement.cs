using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    Vector3 inputDir;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A D
        float v = Input.GetAxisRaw("Vertical");   // W S

        inputDir = new Vector3(h, 0, v);

        // Normalize để tránh chạy chéo nhanh
        Vector3 moveDir = inputDir.normalized;

        transform.position += moveDir * speed * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            transform.position,
            transform.position + inputDir.normalized
        );
    }
}

