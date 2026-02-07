using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    public float forcePower = 500f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Nhấn Space để đẩy lên
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * forcePower);
        }

        // Giữ W để đẩy về phía trước
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * forcePower * Time.deltaTime);
        }
    }
}