using UnityEngine;

public class OneWayPlatformMini : MonoBehaviour
{
    private BoxCollider platformCollider;

    void Start()
    {
        platformCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.position.y > transform.position.y + 0.2f)
            {
                platformCollider.isTrigger = false;
            }
            else
            {
                platformCollider.isTrigger = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformCollider.isTrigger = false;
        }
    }
}
