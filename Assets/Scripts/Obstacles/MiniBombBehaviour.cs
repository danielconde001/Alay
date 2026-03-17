using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBombBehaviour : MonoBehaviour
{
    [SerializeField] private ExplosionBehaviour explosionPrefab;
    [SerializeField] private int damage = 40;

    public void Explode()
    {
        ExplosionBehaviour explosion =
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<ExplosionBehaviour>();

        explosion.Damage = damage; // Dirty as fuck, but whatever..

        Destroy(gameObject);
    }

}
