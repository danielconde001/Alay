using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoredFoodUI : MonoBehaviour
{
    public static StoredFoodUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image storedFoodImage;

    public void UpdateStoreFoodUI(bool isEmpty)
    {
        if (isEmpty)
        {
            storedFoodImage.color = new Color(1, 1, 1, 0);
        }
        else
        {
            storedFoodImage.color = new Color(1, 1, 1, 1);
            storedFoodImage.sprite = PlayerManager.Instance.GetPlayerInventory().FoodInStorage.Image;
        }
    }
}

