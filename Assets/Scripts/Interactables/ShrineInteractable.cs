using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Toast;

public class ShrineInteractable : Interactable
{
    [SerializeField] protected GameObject servingUI;
    [SerializeField] protected GameObject servingUIPrefab;

    public override void Interact()
    {
        base.Interact();

        if (PlayerManager.Instance.GetPlayerInventory().FoodInStorage == null)
        {
            //Toast.Show("You need to cook food before serving!", 2f, Color.red);

            FlowchartManager.Instance.GetMainFlowchart().ExecuteBlock("Serve God with no food");
        }
        else if (PlayerManager.Instance.GetPlayerInventory().FoodInStorage != null)
        {
            if (servingUI == null)
            {
                servingUI = (GameObject)Instantiate(servingUIPrefab);
            }

            GameManager.Instance.servingSystem = servingUI.GetComponent<ServingSystem>();

            FlowchartManager.Instance.GetMainFlowchart().ExecuteBlock("Serve God with food");
        }
    }
}
