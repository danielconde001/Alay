using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    [SerializeField] private Collider2D hitBoxLeft;
    [SerializeField] private Collider2D hitBoxRight;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Attack()
    {
        base.Attack();
        
        List<Collider2D> hits = new List<Collider2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D()
        {
            layerMask = playerLayerMask,
            useLayerMask = true
        };

        if (spriteRenderer.flipX == false)
        {
            Physics2D.OverlapCollider(hitBoxLeft, contactFilter2D, hits);
        }
        else if (spriteRenderer.flipX == true)
        {
            Physics2D.OverlapCollider(hitBoxRight, contactFilter2D, hits);
        }

        for (int i = 0; i < hits.Count; i++)
        {

            hits[i].GetComponent<EntityHealth>()?.DeductHealth(Random.Range(minDamage, maxDamage + 1));
        }
    }
}
