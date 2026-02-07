using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed = 3f;
    public Vector3 direction = Vector3.forward;

    void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }
}
