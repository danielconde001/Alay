using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObjects/Food", order = 1)]
public class FoodData : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public string Description;
    public string Recipe;
}
