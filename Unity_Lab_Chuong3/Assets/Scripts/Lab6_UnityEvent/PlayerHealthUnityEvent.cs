using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthUnityEvent : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;

    public UnityEvent<int> OnHealthChanged;

    void Start()
    {
        currentHp = maxHp;

        // GỌI EVENT SAU KHI SET HP
        OnHealthChanged.Invoke(currentHp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        OnHealthChanged.Invoke(currentHp);
    }
}


