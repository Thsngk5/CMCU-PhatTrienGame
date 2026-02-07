using UnityEngine;

public class ConveyorMini : MonoBehaviour
{
    public float speed = 3f;
    public Vector3 direction = Vector3.forward;

    void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * speed + Vector3.up * rb.velocity.y;
        }

        CharacterController cc = collision.gameObject.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.Move(direction.normalized * speed * Time.deltaTime);
        }
    }
}
