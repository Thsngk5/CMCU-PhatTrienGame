using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform turretHead; // Phần đầu turret sẽ xoay
    
    [Header("Rotation Settings")]
    [SerializeField] private RotationMode rotationMode = RotationMode.LookAt;
    [SerializeField] private float rotationSpeed = 5f; // Cho RotateTowards và Slerp
    
    [Header("Gizmos")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private float rayLength = 5f;
    
    public enum RotationMode
    {
        LookAt,          // Xoay trực tiếp - tức thì
        RotateTowards,   // Xoay mượt với góc cố định mỗi frame
        Slerp            // Xoay mượt với tốc độ phần trăm
    }

    void Update()
    {
        if (target == null || turretHead == null) return;

        switch (rotationMode)
        {
            case RotationMode.LookAt:
                RotateWithLookAt();
                break;
            case RotationMode.RotateTowards:
                RotateWithRotateTowards();
                break;
            case RotationMode.Slerp:
                RotateWithSlerp();
                break;
        }

        // Chuyển đổi chế độ bằng phím số
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            rotationMode = RotationMode.LookAt;
            Debug.Log("<color=cyan>Chế độ: LookAt (Xoay tức thì)</color>");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            rotationMode = RotationMode.RotateTowards;
            Debug.Log("<color=yellow>Chế độ: RotateTowards (Xoay với tốc độ cố định)</color>");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            rotationMode = RotationMode.Slerp;
            Debug.Log("<color=green>Chế độ: Slerp (Xoay mượt mà)</color>");
        }
    }

    // Phương thức 1: LookAt - Xoay trực tiếp, tức thì
    void RotateWithLookAt()
    {
        turretHead.LookAt(target);
        
        // Giữ trục X và Z không bị nghiêng (chỉ xoay trên mặt phẳng Y)
        Vector3 euler = turretHead.eulerAngles;
        turretHead.eulerAngles = new Vector3(0, euler.y, 0);
    }

    // Phương thức 2: RotateTowards - Xoay với tốc độ góc cố định
    void RotateWithRotateTowards()
    {
        // Tính hướng đến target
        Vector3 direction = target.position - turretHead.position;
        direction.y = 0; // Chỉ xoay trên mặt phẳng Y
        
        if (direction == Vector3.zero) return;

        // Quaternion đích
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        // Xoay từ từ với tốc độ cố định (độ/giây)
        float step = rotationSpeed * 50f * Time.deltaTime; // 50 để có tốc độ hợp lý
        turretHead.rotation = Quaternion.RotateTowards(
            turretHead.rotation, 
            targetRotation, 
            step
        );
    }

    // Phương thức 3: Slerp - Xoay mượt mà với tốc độ phần trăm
    void RotateWithSlerp()
    {
        // Tính hướng đến target
        Vector3 direction = target.position - turretHead.position;
        direction.y = 0; // Chỉ xoay trên mặt phẳng Y
        
        if (direction == Vector3.zero) return;

        // Quaternion đích
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        // Xoay mượt mà (t = tốc độ interpolation từ 0-1)
        turretHead.rotation = Quaternion.Slerp(
            turretHead.rotation, 
            targetRotation, 
            rotationSpeed * Time.deltaTime
        );
    }

    void OnDrawGizmos()
    {
        if (!showGizmos || turretHead == null) return;

        // Vẽ hướng turret đang nhìn
        Gizmos.color = Color.red;
        Vector3 forward = turretHead.forward * rayLength;
        Gizmos.DrawRay(turretHead.position, forward);
        
        // Vẽ đường đến target
        if (target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(turretHead.position, target.position);
            
            // Vẽ sphere tại target
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(target.position, 0.5f);
        }

        // Vẽ text hiển thị chế độ
        #if UNITY_EDITOR
        UnityEditor.Handles.Label(
            turretHead.position + Vector3.up * 2, 
            $"Mode: {rotationMode}\nSpeed: {rotationSpeed}",
            new GUIStyle() { normal = new GUIStyleState() { textColor = Color.white } }
        );
        #endif
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 250, 120), "Rotation Test");
        
        if (GUI.Button(new Rect(20, 40, 230, 25), "[1] LookAt - Instant"))
            rotationMode = RotationMode.LookAt;
        
        if (GUI.Button(new Rect(20, 70, 230, 25), "[2] RotateTowards - Fixed Speed"))
            rotationMode = RotationMode.RotateTowards;
        
        if (GUI.Button(new Rect(20, 100, 230, 25), "[3] Slerp - Smooth"))
            rotationMode = RotationMode.Slerp;
    }
}
