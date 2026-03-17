using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableChecker : MonoBehaviour
{
    [SerializeField] protected Interactable assignedInteractable;
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
            InteractableManager.Instance.NearbyInteractables.Add(assignedInteractable);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerIsNear = false;
            InteractableManager.Instance.NearbyInteractables.Remove(assignedInteractable);
        }
    }
}
