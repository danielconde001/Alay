using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseKeyUI : MonoBehaviour
{
    [SerializeField] private GameObject useKeyImage;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        useKeyImage.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
