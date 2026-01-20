using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 180f;
    public bool smoothRotation = true;

    void Update()
    {
        // Hướng từ turret đến target
        Vector3 direction = target.position - transform.position;

        // Tạo rotation cần xoay tới
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        if (smoothRotation)
        {
            // Xoay mượt
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotateSpeed * Time.deltaTime
            );
        }
        else
        {
            // Xoay trực tiếp
            transform.rotation = targetRotation;
        }
    }
}

