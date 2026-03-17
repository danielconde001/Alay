using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBehaviour : MonoBehaviour
{
    protected int damage;
    protected float speed;
    protected Vector2 direction;
    protected float selfDestructTimer;
    protected bool applyBuff = false;

    [SerializeField] protected LayerMask affectedLayerMask;

    [SerializeField] protected ExplosionBehaviour explosionPrefab;

    [SerializeField] GameObject bulletImpactPrefab;

    public virtual void SetupProjectile(Vector2 p_direction, int p_damage, float p_speed, float p_selfDestructTimer, bool p_applyBuff = false)
    {
        direction = p_direction;
        damage = p_damage;
        speed = p_speed;
        selfDestructTimer = p_selfDestructTimer;
        applyBuff = p_applyBuff;

        transform.up = direction;

        StartCoroutine(MoveProjectileCoroutine());
    }

    protected virtual IEnumerator MoveProjectileCoroutine()
    {
        while (selfDestructTimer > 0f)
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
            selfDestructTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (affectedLayerMask == (affectedLayerMask | 1 << collision.gameObject.layer))
        {
            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                collision.gameObject.GetComponent<EntityHealth>().DeductHealth(damage);
            }

            if (applyBuff)
            {
                ExplosionBehaviour explosion =
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<ExplosionBehaviour>();

                explosion.Damage = damage*2; // Dirty as fuck, but whatever..
            }

            Instantiate(bulletImpactPrefab, transform.position, transform.rotation)
                .GetComponent<BulletImpactBehaviour>().spriteRenderer.color =
                GetComponent<SpriteRenderer>().color;

            Destroy(gameObject);
        }
    }
}
