using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDamageable : MonoBehaviour
{
    [SerializeField] private Material hurtMaterial;

    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;
    private IEnumerator flashEffect;
    
    private bool invincible = false;
    public bool Invincible { set => invincible = value; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        flashEffect = FlashEffect();
    }

    public void OnHurt()
    {
        if (!invincible)
        {
            AudioManager.Instance.PlaySFX("Damage");
            StopCoroutine(flashEffect);
            StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        for (int i = 0; i < 10; i++)
        {
            Material hurtMat = hurtMaterial;

            hurtMat.color = Random.Range(0, 100) < 50 ? Color.red : Color.white;

            spriteRenderer.material = hurtMat;
            yield return new WaitForSeconds(0.025f);
        }

        spriteRenderer.material = defaultMaterial;
    }

}
