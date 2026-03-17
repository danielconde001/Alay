using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public FoodType foodType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.Instance.PlaySFX("Picking Up Item");

            if (foodType == FoodType.Meat)
            {
                PlayerManager.Instance.GetPlayerInventory().MeatCapacity += 1;
                CollectiblesUI.Instance.UpdateMeatText();
            }

            else
            {
                PlayerManager.Instance.GetPlayerInventory().VeggieCapacity += 1;
                CollectiblesUI.Instance.UpdateVeggieText();
            }

            Destroy(gameObject);
        }
    }
}
