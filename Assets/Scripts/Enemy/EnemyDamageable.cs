using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour
{
    [SerializeField] private GameObject hurtParticlePrefab;
    [SerializeField] private Material hurtMaterial;

    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;
    private IEnumerator flashEffect;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        flashEffect = FlashEffect();
    }

    public void OnDeath()
    {
        AudioManager.Instance.PlaySFX("Enemy Death");
        gameObject.SetActive(false);
    }

    public void OnHurt()
    {
        ParticleSystem hurtParticle = Instantiate(hurtParticlePrefab, this.transform).GetComponent<ParticleSystem>();
        hurtParticle.Play();

        AudioManager.Instance.PlaySFX("Damage");

        // Do the flash effect
        StopCoroutine(flashEffect);
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        Material hurtMat = hurtMaterial;
        spriteRenderer.material = hurtMat;

        for (int i = 0; i < 10; i++)
        { 
            spriteRenderer.material.color = Random.Range(0, 100) < 50 ? Color.red : Color.white;

            yield return new WaitForSeconds(0.025f);
        }

        spriteRenderer.material = defaultMaterial;
    }
}
