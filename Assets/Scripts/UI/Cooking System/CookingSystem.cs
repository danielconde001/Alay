using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum FoodType
{
    Meat,
    FruitsAndVegetables,
    None,
    Count
}

public class CookingSystem : MonoBehaviour
{
    [SerializeField] private PotBehaviour pot;
    public PotBehaviour Pot { get => pot; }

    [SerializeField] private List<FoodInventorySlot> foodInventorySlots = new List<FoodInventorySlot>();
    public List<FoodInventorySlot> FoodInventorySlots
    {
        get => foodInventorySlots;
    }

    private bool isDraggingFood = false;
    private bool IsDraggingFood { set { IsDraggingFood = value; } }

    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform defaultPoint;

    [SerializeField] private NewFoodWindow newFoodWindow;
    [SerializeField] private GameObject takeButton;
    [SerializeField] private GameObject leftWindow;

    [SerializeField] private List<FoodData> possibleFoods;
    [SerializeField] private UnityEvent OnCookEnd;

    private FoodData cookedFood;

    private void OnEnable()
    {
        PlayerManager.Instance.SetPlayerControls(false);

        // reset stuff
        takeButton.SetActive(false);
        leftWindow.SetActive(true);
        pot.gameObject.transform.position = defaultPoint.position;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.SetPlayerControls(true);
    }

    // Anim event
    public void StartCook()
    {
        string ingredients = pot.GetIngredients();
        List<FoodData> candidates = new List<FoodData>();

        for (int i = 0; i < possibleFoods.Count; i++)
        {
            if (possibleFoods[i].Recipe == ingredients)
            {
                candidates.Add(possibleFoods[i]);
            }
        }

        pot.EnteredFood.Clear();

        FoodData data = candidates[Random.Range(0, candidates.Count)];
        cookedFood = data;

        newFoodWindow.gameObject.SetActive(true);
        newFoodWindow.InjectData(data.Name, data.Image, data.Description);

        pot.transform.DOMove(centerPoint.position, 1f).OnComplete( () =>  DuringCook());
    }

    private void DuringCook()
    {
        AudioManager.Instance.PlaySFX("Cooking");
        pot.Animator.SetTrigger("Cook");
    }

    public void ShowCookedFood()
    {
        AudioManager.Instance.PlaySFX("Paper");
        AudioManager.Instance.PlaySFX("Cooking Success");
        newFoodWindow.transform.DOLocalMoveY(0, 1f).OnComplete( () => takeButton.SetActive(true)); 
    }

    public void EndCook()
    {
        AudioManager.Instance.PlaySFX("Paper");
        PlayerManager.Instance.GetPlayerInventory().FoodInStorage = cookedFood;
        StoredFoodUI.Instance.UpdateStoreFoodUI(false);
        CollectiblesUI.Instance.UpdateMeatText();
        CollectiblesUI.Instance.UpdateVeggieText();
        //OnCookEnd.Invoke();
        gameObject.SetActive(false);
    }
}
