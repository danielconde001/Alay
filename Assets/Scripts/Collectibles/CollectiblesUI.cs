using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesUI : MonoBehaviour
{
    public static CollectiblesUI Instance;

    [SerializeField] Text veggieText;
    [SerializeField] Text meatText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateVeggieText()
    {
        veggieText.text = PlayerManager.Instance.GetPlayerInventory().VeggieCapacity.ToString();
    }

    public void UpdateMeatText()
    {
        meatText.text = PlayerManager.Instance.GetPlayerInventory().MeatCapacity.ToString();
    }
}
