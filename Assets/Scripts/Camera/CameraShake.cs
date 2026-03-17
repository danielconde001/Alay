using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraShake : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Shake() 
    {
        animator.enabled = true;
        animator.SetTrigger("CameraShake");
    }

    public void OnEnd()
    {
        animator.enabled = false;
    }
}
