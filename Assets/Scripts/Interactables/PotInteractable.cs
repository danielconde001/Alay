using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotInteractable : Interactable
{
    [SerializeField] protected GameObject cookingUI;
    [SerializeField] protected GameObject cookingUIPrefab;

    public override void Interact()
    {
        base.Interact();

        if (cookingUI == null)
        {
            cookingUI = (GameObject)Instantiate(cookingUIPrefab);
        }
        
        cookingUI.SetActive(true);
    }
}
