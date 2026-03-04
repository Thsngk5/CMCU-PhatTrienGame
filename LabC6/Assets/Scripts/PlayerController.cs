using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3f;

    [Header("References")]
    public GameObject fxAttack; // kéo FX_Attack vào đây trong Inspector

    // Components
    private Animator        anim;
    private SpriteRenderer  sr;

    // Hash (tối ưu, không dùng string mỗi frame)
    private static readonly int SpeedHash  = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    void Start()
    {
        anim = GetComponent<Animator>();
        sr   = GetComponent<SpriteRenderer>();

        // Đảm bảo FX tắt lúc đầu
        if (fxAttack != null)
            fxAttack.SetActive(false);
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");

        // Di chuyển
        transform.Translate(Vector2.right * h * moveSpeed * Time.deltaTime);

        // Lật sprite theo hướng
        if      (h >  0.01f) sr.flipX = false; // nhìn phải
        else if (h < -0.01f) sr.flipX = true;  // nhìn trái

        // Cập nhật Speed cho Animator
        anim.SetFloat(SpeedHash, Mathf.Abs(h));
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(AttackHash);

            // Hiện FX
            if (fxAttack != null)
            {
                fxAttack.SetActive(true);
                Invoke(nameof(HideFX), 0.4f); // tắt sau 0.4 giây
            }
        }
    }

    void HideFX()
    {
        if (fxAttack != null)
            fxAttack.SetActive(false);
    }
}
