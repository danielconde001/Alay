using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnteredFoodSlot : MonoBehaviour
{
    private PotBehaviour pot;
    
    private bool isEmpty = true;
    public bool IsEmpty
    {
        set => isEmpty = value;
    }

    [SerializeField] private Image enteredFoodIcon;
    [SerializeField] private int slotIndex;
    [SerializeField] private Sprite meatSprite;
    [SerializeField] private Sprite veggieSprite;

    private void Awake()
    {
        pot = GetComponentInParent<PotBehaviour>();
    }

    public void OnPress()
    {
        if (!isEmpty)
        {
            for (int i = 0; i < pot.CookingSystem.FoodInventorySlots.Count; i++)
            {
                if (pot.CookingSystem.FoodInventorySlots[i].FoodInSlot == pot.EnteredFood[slotIndex])
                {
                    pot.CookingSystem.FoodInventorySlots[i].AddFood();
                }
            }
            pot.RemoveFromPot(slotIndex);
            
        }
    }

    public void UpdateIcon(FoodType p_foodType)
    {
        if (p_foodType == FoodType.Meat)
        {
            enteredFoodIcon.color = new Color(1,1,1,1);
            enteredFoodIcon.sprite = meatSprite;
            isEmpty = false;
        }

        else if (p_foodType == FoodType.FruitsAndVegetables)
        {
            enteredFoodIcon.color = new Color(1, 1, 1, 1);
            enteredFoodIcon.sprite = veggieSprite;
            isEmpty = false;
        }

        else
        {
            enteredFoodIcon.color = new Color(1, 1, 1, 0);
            enteredFoodIcon.sprite = null;
            isEmpty = true;
        }
    }
}
