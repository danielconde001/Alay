using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AnimatorParameter
{
    INTEGER,
    FLOAT,
    BOOLEAN,
    TRIGGER,
    Count
}

[RequireComponent(typeof(Animator))]
public class AnimationPlayDebug : MonoBehaviour
{
    [SerializeField] private AnimatorParameter parameter;
    [SerializeField] private KeyCode debugKey;
    [SerializeField] private string animationName;
    [SerializeField] private float parameterValue;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(debugKey))
        {
            TestAnimation(parameter, parameterValue);
        }
    }

    private void TestAnimation(AnimatorParameter p_parameter, float p_value)
    {
        if (p_parameter == AnimatorParameter.INTEGER)
        {
            animator.SetInteger(animationName, (int)p_value);
        }
        else if (p_parameter == AnimatorParameter.FLOAT)
        {
            animator.SetFloat(animationName, p_value);
        }
        else if (p_parameter == AnimatorParameter.BOOLEAN)
        {
            animator.SetBool(animationName, Convert.ToBoolean((int)p_value));
        }
        else if (p_parameter == AnimatorParameter.TRIGGER)
        {
            animator.SetTrigger(animationName);
        }
    }

}
