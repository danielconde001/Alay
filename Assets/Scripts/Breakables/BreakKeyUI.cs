using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakKeyUI : MonoBehaviour
{
    [SerializeField] private GameObject breakKeyImage;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        breakKeyImage.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
