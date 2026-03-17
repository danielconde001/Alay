using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour, IKillable
{
    [SerializeField] protected int healthPoints;

    private int currentHealthPoints;
    public int CurrentHealthPoints
    {
        get => currentHealthPoints;
    }

    private int maxHealthPoints;
    public int MaxHealthPoints
    {
        get => maxHealthPoints;
    }

    protected virtual void Awake()
    {
        maxHealthPoints = healthPoints;
        currentHealthPoints = maxHealthPoints;
    }

    public virtual void DeductHealth(int deduction)
    {
        currentHealthPoints -= deduction;

        if (currentHealthPoints <= 0)
        {
            Death();
        }

        else if (currentHealthPoints >= maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }
    }

    public virtual void Death()
    {

    }
}
