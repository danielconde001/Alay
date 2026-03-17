using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseDamageable : MonoBehaviour
{
    [SerializeField] private float flashInterval;
    [SerializeField] private Sprite damagedVaseSprite;
    private SpriteRenderer spriteRenderer;
    private EntityHealth health;

    [SerializeField] GameObject hurtParticlePrefab;

    [SerializeField] GameObject[] collectibles;
    [SerializeField] Material hurtMaterial;

    Material defaultMaterial;

    IEnumerator hurtCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        health = GetComponent<EntityHealth>();
        hurtCoroutine = HurtCoroutine();
    }

    public void OnDeath()
    {
        // drop shit
        int index = Random.Range(0, 100) < 50 ? 0 : 1;
        Instantiate(collectibles[index], transform.position, Quaternion.identity);

        AudioManager.Instance.PlaySFX("Breakables Breaking");

        Destroy(gameObject);
    }

    public void OnHurt()
    {
        //ParticleSystem hurtParticle = Instantiate(hurtParticlePrefab, this.transform).GetComponent<ParticleSystem>();
        //hurtParticle.Play();
        StopCoroutine(hurtCoroutine);
        StartCoroutine(HurtCoroutine());
    }

    private IEnumerator HurtCoroutine()
    {
        if (health.CurrentHealthPoints < (health.MaxHealthPoints))
        {
            spriteRenderer.sprite = damagedVaseSprite;
        }

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
