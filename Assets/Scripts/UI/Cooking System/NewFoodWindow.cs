using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFoodWindow : MonoBehaviour
{
    [SerializeField] private Text FoodName;
    [SerializeField] private Image FoodImage;
    [SerializeField] private Text FoodDescription;

    private void OnDisable()
    {
        transform.localPosition = new Vector3(0, -1030);
    }

    public void InjectData(string p_name, Sprite p_image, string p_description)
    {
        FoodName.text = p_name;
        FoodImage.sprite = p_image;
        FoodDescription.text = p_description;
    }
}
