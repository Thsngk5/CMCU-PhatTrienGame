using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlLab8 : MonoBehaviour
{
    private Animator anim;

    private int speedHash;
    private int attackHash;

    private float lastSpeed = -1f;

    void Start()
    {
        anim = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
        attackHash = Animator.StringToHash("Attack");
    }
    void Update()
    {
        float CurrentSpeed = Mathf.Abs(Input.GetAxis("Horizontal"));
        if (CurrentSpeed != lastSpeed)
        {
            anim.SetFloat(speedHash, CurrentSpeed);
            lastSpeed = CurrentSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(attackHash);
        }
    }
}