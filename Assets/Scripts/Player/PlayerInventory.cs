using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private int meatCapacity = 0;
    public int MeatCapacity
    {
        get => meatCapacity;
        set => meatCapacity = value;
    }

    private int veggieCapacity = 0;
    public int VeggieCapacity
    {
        get => veggieCapacity;
        set => veggieCapacity = value;
    }

    private FoodData foodInStorage = null;
    public FoodData FoodInStorage
    {
        get => foodInStorage;
        set => foodInStorage = value;
    }
}
