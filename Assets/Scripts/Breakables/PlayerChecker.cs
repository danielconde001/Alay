using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerChecker : MonoBehaviour
{
    [SerializeField] protected Breakable assignedBreakable;
    protected Collider2D col;
    protected bool playerIsNear;

    public bool PlayerIsNear
    {
        get { return playerIsNear; }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerIsNear = true;
            BreakableManager.Instance.NearbyBreakables.Add(assignedBreakable);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerIsNear = false;
            BreakableManager.Instance.NearbyBreakables.Remove(assignedBreakable);
        }
    }
}
