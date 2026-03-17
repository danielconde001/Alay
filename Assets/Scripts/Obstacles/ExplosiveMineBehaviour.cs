using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExplosiveMineBehaviour : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask triggerableLayerMask;
    [SerializeField] private LayerMask affectedlayerMask;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int damage;

    private Animator animator;
    private bool entered = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Activate()
    {
        animator.SetTrigger("Explode");
    }

    public void PlayExplosionSFX()
    {
        AudioManager.Instance.PlaySFX("Explosion");
    }

    public void ApplyHitbox()
    {
        List<Collider2D> hits =
            Physics2D.OverlapCircleAll(transform.position + offset, explosionRadius, affectedlayerMask).ToList();

        for (int i = 0; i < hits.Count; i++)
        {
            hits[i].GetComponent<EntityHealth>()?.DeductHealth(damage);
        }
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entered)
        {
            return;
        }

        if (triggerableLayerMask == (triggerableLayerMask | 1 << collision.gameObject.layer))
        {
            entered = true;
            Activate();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, explosionRadius);
    }
}
