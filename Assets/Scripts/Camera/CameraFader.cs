using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraFader : MonoBehaviour
{
    public static CameraFader Instance;

    [SerializeField] private Image FadeImage;

    private void Awake()
    {
        Instance = this;   
    }

    public void FadeOut()
    {
        FadeImage.DOColor(Color.black, 1f);
    }

    public void FadeIn()
    {
        FadeImage.DOColor(Color.clear, 1f);
    }
}
