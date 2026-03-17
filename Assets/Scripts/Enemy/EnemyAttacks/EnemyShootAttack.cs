using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttack : EnemyAttack
{
    [SerializeField] private int damage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private bool doesSpread;
    [SerializeField] private float projectileSpread;
    [SerializeField] private int numberOfProjectiles;

    [SerializeField] private GameObject projectilePrefab;

    public override void Attack()
    {
        base.Attack();

        Vector2 direction = (PlayerManager.Instance.GetPlayer().transform.position - transform.position).normalized;

        float facingRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float startRotation = facingRotation + projectileSpread / 2f;
        float angleIncrease = projectileSpread / ((float)numberOfProjectiles - 1f);
        if (doesSpread)
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float tempRot = startRotation - angleIncrease * i;

                ProjectileBehaviour projectile =
                    Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBehaviour>();

                direction = new Vector2(Mathf.Cos(tempRot * Mathf.Deg2Rad), Mathf.Sin(tempRot * Mathf.Deg2Rad));

                projectile.SetupProjectile(direction, damage, projectileSpeed, 5f);
            }
        }
        else 
        {
            ProjectileBehaviour projectile =
                    Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBehaviour>();
            projectile.SetupProjectile(direction, damage, projectileSpeed, 5f);
        }
    }
}
