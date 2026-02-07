using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private BoxCollider platformCollider;

    void Start()
    {
        platformCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerStay(Collider other)
    {
        // Nếu Player ở phía trên platform → bật collision
        if (other.transform.position.y > transform.position.y + 0.5f)
        {
            platformCollider.isTrigger = false;
        }
        else
        {
            platformCollider.isTrigger = true;
        }
    }
}
