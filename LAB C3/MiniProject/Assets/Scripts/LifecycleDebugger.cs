using UnityEngine;

/// <summary>
/// Mini Project - Lifecycle Debugger
/// Lab 1: Ghi log tất cả lifecycle methods
/// Attach vào bất kỳ GameObject nào để debug
/// </summary>
public class LifecycleDebugger : MonoBehaviour
{
    [Header("Debug Settings")]
    [SerializeField] private string objectName = "GameObject";
    [SerializeField] private Color gizmoColor = Color.magenta;
    
    void Awake()
    {
        Debug.Log($"<color=cyan>[{objectName}]</color> Awake - Time: {Time.time:F3}s");
    }
    
    void OnEnable()
    {
        Debug.Log($"<color=green>[{objectName}]</color> OnEnable - Time: {Time.time:F3}s");
    }
    
    void Start()
    {
        Debug.Log($"<color=yellow>[{objectName}]</color> Start - Time: {Time.time:F3}s");
    }
    
    void FixedUpdate()
    {
        // Chỉ log mỗi giây để tránh spam
        if (Time.fixedTime % 1f < Time.fixedDeltaTime)
        {
            Debug.Log($"<color=blue>[{objectName}]</color> FixedUpdate - FixedTime: {Time.fixedTime:F1}s");
        }
    }
    
    void Update()
    {
        // Chỉ log mỗi giây
        if (Time.time % 1f < Time.deltaTime)
        {
            Debug.Log($"<color=white>[{objectName}]</color> Update - Time: {Time.time:F1}s");
        }
    }
    
    void LateUpdate()
    {
        // Chỉ log mỗi giây
        if (Time.time % 1f < Time.deltaTime)
        {
            Debug.Log($"<color=grey>[{objectName}]</color> LateUpdate - Time: {Time.time:F1}s");
        }
    }
    
    void OnDisable()
    {
        Debug.Log($"<color=orange>[{objectName}]</color> OnDisable - Time: {Time.time:F3}s");
    }
    
    void OnDestroy()
    {
        Debug.Log($"<color=red>[{objectName}]</color> OnDestroy - Time: {Time.time:F3}s");
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}