using UnityEngine;

public class SignedAngleRotate : MonoBehaviour
{
    public Transform target;
    public float currentAngle;

    void Update()
    {
        Debug.Log("Angle: " + currentAngle);
        // Vector hướng từ player đến target (trong mặt phẳng XZ)
        Vector3 dir3D = target.position - transform.position;

        Vector2 dir2D = new Vector2(dir3D.x, dir3D.z);

        // Góc giữa hướng UP (0 độ) và hướng target
        currentAngle = Vector2.SignedAngle(Vector2.up, dir2D);

        // Xoay quanh trục Y (top-down)
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);
    }
}

