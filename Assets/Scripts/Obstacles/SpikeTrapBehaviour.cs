using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpikeTrapBehaviour : MonoBehaviour
{
    [SerializeField] private Collider2D hitBoxCollider;
    [SerializeField] private LayerMask triggerableLayerMask;
    [SerializeField] private LayerMask affectedlayerMask;
    [SerializeField] private int damage;
    [SerializeField] private float activationTime = 5f;

    [SerializeField] private bool autoDestroy = false;

    private Animator animator;
    private bool activated = false;
    public int Activated
    {
        set => activated = System.Convert.ToBoolean(value);
    }

    private bool active = false;
    public int Active
    {
        set => active = System.Convert.ToBoolean(value);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Activate()
    {
        animator.SetTrigger("TriggerSpikeTrap");
    }

    private void Reset()
    {
        animator.SetTrigger("ResetSpikes");
    }

    public void ApplyHitbox()
    {
        List<Collider2D> hits = new List<Collider2D>();

        ContactFilter2D contactFilter2D = new ContactFilter2D()
        {
            layerMask = affectedlayerMask,
            useLayerMask = true
        };

        Physics2D.OverlapCollider(hitBoxCollider, contactFilter2D, hits);
        
        for (int i = 0; i < hits.Count; i++)
        {
            hits[i].GetComponent<EntityHealth>().DeductHealth(damage);
        }

        active = true;

        if (autoDestroy)
        {
            Invoke("SelfDestroy", 1f);
        }

        Invoke("Reset", activationTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerableLayerMask == (triggerableLayerMask | 1 << collision.gameObject.layer) && !activated)
        {
            activated = true;
            Activate();
        }
        else if (affectedlayerMask == (affectedlayerMask | 1 << collision.gameObject.layer) && active)
        {
            collision.GetComponent<EntityHealth>().DeductHealth(damage);
        }
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
