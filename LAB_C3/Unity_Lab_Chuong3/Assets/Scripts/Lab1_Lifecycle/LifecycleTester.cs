using UnityEngine;

public class LifecycleTester : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject prefabToInstantiate;
    
    private GameObject instantiatedObject;

    void Update()
    {
        // Phím T - Toggle Active/Inactive
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf);
                Debug.Log($"<color=yellow>>>> Toggle Object: {targetObject.activeSelf}</color>");
            }
        }

        // Phím D - Destroy Object
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (targetObject != null)
            {
                Debug.Log($"<color=red>>>> Destroying Object</color>");
                Destroy(targetObject);
            }
        }

        // Phím I - Instantiate Object
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (prefabToInstantiate != null)
            {
                Debug.Log($"<color=green>>>> Instantiating Object</color>");
                instantiatedObject = Instantiate(prefabToInstantiate);
            }
        }

        // Phím X - Destroy Instantiated Object
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (instantiatedObject != null)
            {
                Debug.Log($"<color=red>>>> Destroying Instantiated Object</color>");
                Destroy(instantiatedObject);
                instantiatedObject = null;
            }
        }
    }
}
