using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : EntityHealth
{
    [SerializeField] protected EnemyHPBar enemyHPBar;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void DeductHealth(int deduction)
    {
        base.DeductHealth(deduction);

        enemyHPBar.SetHPBarValue((float)CurrentHealthPoints / (float)MaxHealthPoints);
    }
}
