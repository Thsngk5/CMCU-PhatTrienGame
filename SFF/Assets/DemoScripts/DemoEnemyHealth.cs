using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected override void Die()
    {
        base.Die();
        LivingEnemyCount--;
        Debug.Log("Enemy died");
    }
    public static int LivingEnemyCount;
    private void Awake() => LivingEnemyCount++;

}

