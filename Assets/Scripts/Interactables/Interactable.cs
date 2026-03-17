using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected InteractableChecker interactableChecker;
    [SerializeField] protected GameObject useKeyObject;
    [SerializeField] protected KeyCode useKey;

    protected virtual void Update()
    {
        if (interactableChecker.PlayerIsNear)
        {
            if (InteractableManager.Instance.NearbyInteractables[0] == this)
            {
                useKeyObject.SetActive(true);
                if (Input.GetKeyDown(useKey))
                {
                    Interact();
                }
            }
        }
        else
        {
            useKeyObject.SetActive(false);
        }
    }

    public virtual void Interact()
    {
        
    }
}
