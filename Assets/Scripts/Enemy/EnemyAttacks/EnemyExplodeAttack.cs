using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplodeAttack : EnemyAttack
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;

    protected override void Awake()
    {
        base.Awake();

        enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Attack()
    {
        base.Attack();

        ExplosionBehaviour explosion = 
            Instantiate(explosionPrefab, transform.position + spawnOffset, Quaternion.identity).GetComponent<ExplosionBehaviour>();

        explosion.Damage = Random.Range(minDamage, maxDamage - 1);

        enemyHealth.GetOnDeathEvent().Invoke();

        //Destroy(gameObject);
    }
}
