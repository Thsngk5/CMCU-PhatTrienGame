using UnityEngine;

public class TurretRotateTopDown : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 180f;

    void Update()
    {
        Vector3 dir3D = target.position - transform.position;
        Vector2 dir2D = new Vector2(dir3D.x, dir3D.z);

        float angle = Vector2.SignedAngle(Vector2.up, dir2D);
        angle = -angle;
        Quaternion targetRot = Quaternion.Euler(0, angle, 0);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime
        );
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.forward * 2f
        );
    }
}

