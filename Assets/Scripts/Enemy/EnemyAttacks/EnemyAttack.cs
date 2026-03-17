using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float attackRate;
    public float AttackRate
    {
        get => attackRate;
    }

    protected float attackRateCounter;
    public float AttackRateCounter
    {
        get => attackRateCounter;
        set => attackRateCounter = value;
    }

    protected bool isAttacking = false;
    public int IsAttacking
    {
        get => Convert.ToInt32(isAttacking);
        set => isAttacking = Convert.ToBoolean(value);
    }

    protected virtual void Awake()
    {
        attackRateCounter = attackRate;
    }

    public void RefreshAttackRateCounter()
    {
        attackRateCounter = attackRate;
    }

    public virtual void Attack()
    {
        
    }
}
