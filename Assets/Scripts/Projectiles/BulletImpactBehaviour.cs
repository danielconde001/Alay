using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
