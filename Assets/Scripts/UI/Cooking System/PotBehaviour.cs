using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class PotBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CookingSystem cookingSystem;
    public CookingSystem CookingSystem
    {
        get { return cookingSystem; }
    }

    private bool isHoveringOverPot = false;
    public bool IsHoveringOverPot { get { return isHoveringOverPot; } }

    private List<FoodType> enteredFood = new List<FoodType>();
    public List<FoodType> EnteredFood
    {
        get => enteredFood;
    }

    [SerializeField] private List<EnteredFoodSlot> enteredFoodSlots = new List<EnteredFoodSlot>();
    [SerializeField] private Button cookButton;
    [SerializeField] private GameObject enteredFoodParent;

    private Animator animator;
    public Animator Animator
    {
        get => animator;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        cookButton.gameObject.SetActive(false);
        enteredFoodParent.SetActive(true);
    }

    private void OnDisable()
    {
        for (int i = 0; i < enteredFood.Count; i++)
        {
            if (enteredFood[i] == FoodType.Meat)
            {
                PlayerManager.Instance.GetPlayerInventory().MeatCapacity += 1;
            }
            else if (enteredFood[i] == FoodType.FruitsAndVegetables)
            {
                PlayerManager.Instance.GetPlayerInventory().VeggieCapacity += 1;
            }
        }

        for (int i = 0; i < enteredFoodSlots.Count; i++)
        {
            enteredFoodSlots[i].UpdateIcon(FoodType.None);
            enteredFoodSlots[i].gameObject.SetActive(false);
        }

        enteredFood.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringOverPot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringOverPot = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DragFood")
        {
            isHoveringOverPot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DragFood")
        {
            isHoveringOverPot = false;
        }
    }

    public int GetCapacity()
    {
       return enteredFood.Count;
    }

    public string GetIngredients()
    {
        string ingredients = "";

        for (int i = 0; i < enteredFood.Count; i++)
        {
            if (enteredFood[i] == FoodType.Meat)
            {
                ingredients += "M";
            }
        }

        for (int i = 0; i < enteredFood.Count; i++)
        {
            if (enteredFood[i] == FoodType.FruitsAndVegetables)
            {
                ingredients += "V";
            }
        }

        return ingredients;
    }

    public void DropInPot(FoodType p_foodType)
    {
        if (enteredFood.Count < 4)
        {
            enteredFood.Add(p_foodType);
        }
        else
        {
            // Show pot is full modal.
        }

        UpdateEnteredFoodSlots();
    }

    public void RemoveFromPot(int p_index)
    {
        enteredFood.RemoveAt(p_index);

        UpdateEnteredFoodSlots();
    }

    // Anim event
    public void OnCookEnd()
    {
        CookingSystem.ShowCookedFood();
    }

    private void UpdateEnteredFoodSlots()
    {
        if (enteredFood.Count > 0)
        {
            for (int i = 0; i < enteredFoodSlots.Count; i++)
            {
                enteredFoodSlots[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (i < enteredFood.Count)
            {
                enteredFoodSlots[i].UpdateIcon(enteredFood[i]);
            }
            else
            {
                enteredFoodSlots[i].UpdateIcon(FoodType.None);
            }
        }

        if (enteredFood.Count <= 0)
        {
            for (int i = 0; i < enteredFoodSlots.Count; i++)
            {
                enteredFoodSlots[i].gameObject.SetActive(false);
            }
        }

        UpdateCookButton();
    }

    private void UpdateCookButton()
    {
        if (enteredFood.Count >= 1)
        {
            cookButton.gameObject.SetActive(true);
        }
        else
        {
            cookButton.gameObject.SetActive(false);
        }
    }
}
