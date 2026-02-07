using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter: " + other.gameObject.name);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Trigger Stay: " + other.gameObject.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger Exit: " + other.gameObject.name);
    }
}