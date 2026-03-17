using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class FoodInventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CookingSystem cookingSystem;
    [SerializeField] private TMP_Text capacityText;
    [SerializeField] private GameObject dragFoodPrefab;

    [SerializeField] private FoodType foodInSlot;
    public FoodType FoodInSlot
    {
        get => foodInSlot;
    }

    private GameObject draggedFood;

    private IEnumerator dropCoroutine;

    private void Awake()
    {
        dropCoroutine = DropCoroutine();
        UpdateCapacityText();
    }

    private void OnEnable()
    {
        UpdateCapacityText();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        draggedFood = Instantiate(dragFoodPrefab, cookingSystem.transform);
        SubtractFood();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(draggedFood);
        Drop();
    }

    private void Drop()
    {
        StartCoroutine(DropCoroutine());
    }

    private IEnumerator DropCoroutine()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        if (cookingSystem.Pot.IsHoveringOverPot && cookingSystem.Pot.GetCapacity() < 4 )
        {
            cookingSystem.Pot.DropInPot(foodInSlot);
        }
        else 
        {
            AddFood();
        }
    }

    private void UpdateCapacityText()
    {
        if (foodInSlot == FoodType.Meat)
        {
            capacityText.text = PlayerManager.Instance.GetPlayerInventory().MeatCapacity.ToString();
        }
        else if (foodInSlot == FoodType.FruitsAndVegetables)
        {
            capacityText.text = PlayerManager.Instance.GetPlayerInventory().VeggieCapacity.ToString();
        }
        else
        {
            Debug.LogError("Wrong FoodType assigned!", this);
        }
    }

    public void SubtractFood()
    {
        if (foodInSlot == FoodType.Meat)
        {
            PlayerManager.Instance.GetPlayerInventory().MeatCapacity--;
        }
        else if (foodInSlot == FoodType.FruitsAndVegetables)
        {
            PlayerManager.Instance.GetPlayerInventory().VeggieCapacity--;
        }
        else
        {
            Debug.LogError("Wrong FoodType assigned!", this);
        }

        UpdateCapacityText();
    }

    public void AddFood()
    {
        if (foodInSlot == FoodType.Meat)
        {
            PlayerManager.Instance.GetPlayerInventory().MeatCapacity++;
        }
        else if (foodInSlot == FoodType.FruitsAndVegetables)
        {
            PlayerManager.Instance.GetPlayerInventory().VeggieCapacity++;
        }
        else
        {
            Debug.LogError("Wrong FoodType assigned!", this);
        }

        UpdateCapacityText();
    }

    private void Update()
    {
        if (draggedFood != null)
        {
            draggedFood.transform.position = Input.mousePosition;
        }
    }
}
