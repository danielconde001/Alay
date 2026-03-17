using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float staminaPoints;
    [SerializeField] private float staminaRegenRate;

    [SerializeField] private UnityEngine.Events.UnityEvent OnRegenEvent;

    private float currentStaminaPoints;
    public float CurrentStaminaPoints
    {
        get => currentStaminaPoints;
    }

    private float maxStaminaPoints;
    public float MaxStaminaPoints
    {
        get => maxStaminaPoints;
    }

    private void Awake()
    {
        maxStaminaPoints = staminaPoints;
        currentStaminaPoints = maxStaminaPoints;
    }

    private void Update()
    {
        if (currentStaminaPoints < maxStaminaPoints)
        {
            currentStaminaPoints += staminaRegenRate * Time.deltaTime;
            OnRegenEvent.Invoke();
        }
    }

    public void DeductStamina(float p_deduction)
    {
        if (p_deduction < currentStaminaPoints)
        {
            currentStaminaPoints -= p_deduction;
        }
    }
}
