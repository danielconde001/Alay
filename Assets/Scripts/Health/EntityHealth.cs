using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : Health
{
    [SerializeField] protected UnityEvent OnDeathEvent;
    public UnityEvent GetOnDeathEvent() { return OnDeathEvent; }

    [SerializeField] protected UnityEvent OnHurtEvent;

    public override void DeductHealth(int deduction)
    {
        base.DeductHealth(deduction);

        if (healthPoints > 0)
        {
            OnHurtEvent.Invoke();
        }
    }

    public override void Death()
    {
        base.Death();
        OnDeathEvent.Invoke();
    }
}
