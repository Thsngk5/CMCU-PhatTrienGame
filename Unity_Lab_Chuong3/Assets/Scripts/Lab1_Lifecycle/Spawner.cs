using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(prefab);
        }
    }
}


