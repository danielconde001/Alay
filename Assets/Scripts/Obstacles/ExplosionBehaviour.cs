using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExplosionBehaviour : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] Vector3 offset;

    private int damage;
    public int Damage 
    { 
        set => damage = value; 
    }

    public void ApplyHitbox()
    {
        List<Collider2D> hits =
            Physics2D.OverlapCircleAll(transform.position + offset, explosionRadius, layerMask).ToList();

        for (int i = 0; i < hits.Count; i++)
        {
            Debug.Log(hits[i].name);
            hits[i].GetComponent<EntityHealth>()?.DeductHealth(damage);
        }
        
    }

    public void PlayExplosionSFX()
    {
        AudioManager.Instance.PlaySFX("Explosion");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, explosionRadius);
    }
}
