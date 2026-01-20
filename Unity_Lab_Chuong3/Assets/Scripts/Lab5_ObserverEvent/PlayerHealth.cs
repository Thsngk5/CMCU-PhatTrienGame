using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;

    // EVENT (Observer Pattern)
    public event Action<int> OnHealthChanged;

    void Start()
    {
        currentHp = maxHp;
        OnHealthChanged?.Invoke(currentHp);
    }

    void Update()
    {
        // Nhấn H để trừ máu
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        // THÔNG BÁO cho các Observer
        OnHealthChanged?.Invoke(currentHp);
    }
}

