using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeAttack : EnemyAttack
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Attack()
    {
        base.Attack();

        // Charge
    }

}
