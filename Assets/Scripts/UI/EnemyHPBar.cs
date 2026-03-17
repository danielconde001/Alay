using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Vector3 offset;

    public void SetHPBarValue(float p_value)
    {
        hpBar.value = p_value;
    }

    private void Update()
    {
        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
