using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    private static InteractableManager instance;
    public static InteractableManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newObj = new GameObject("InteractableManager");
                instance = newObj.AddComponent<InteractableManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private List<Interactable> nearbyInteractables = new List<Interactable>();
    public List<Interactable> NearbyInteractables
    {
        get => nearbyInteractables;
    }
}
