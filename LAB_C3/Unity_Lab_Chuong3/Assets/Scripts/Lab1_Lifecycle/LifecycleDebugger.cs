using UnityEngine;

public class LifecycleDebugger : MonoBehaviour
{
    [SerializeField] private string objectName = "LifecycleObject";
    
    void Awake()
    {
        Debug.Log($"[{objectName}] Awake - Được gọi khi object được khởi tạo");
    }

    void OnEnable()
    {
        Debug.Log($"[{objectName}] OnEnable - Được gọi khi object được kích hoạt");
    }

    void Start()
    {
        Debug.Log($"[{objectName}] Start - Được gọi trước frame đầu tiên");
    }

    void FixedUpdate()
    {
        Debug.Log($"[{objectName}] FixedUpdate - Được gọi với tần số cố định (physics)");
    }

    void Update()
    {
        Debug.Log($"[{objectName}] Update - Được gọi mỗi frame");
    }

    void LateUpdate()
    {
        Debug.Log($"[{objectName}] LateUpdate - Được gọi sau tất cả Update");
    }

    void OnDisable()
    {
        Debug.Log($"[{objectName}] OnDisable - Được gọi khi object bị vô hiệu hóa");
    }

    void OnDestroy()
    {
        Debug.Log($"[{objectName}] OnDestroy - Được gọi khi object bị hủy");
    }
}
