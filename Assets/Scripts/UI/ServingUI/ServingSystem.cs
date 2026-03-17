using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ServingSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> cards;

    public void StartServe()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.localScale = new Vector3(0, cards[i].transform.localScale.y, 1);
        }

        PlayerManager.Instance.SetPlayerControls(false);

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.DOScaleX(1.5f, .5f);
        }
    }

    public void CloseServe()
    {
        PlayerManager.Instance.SetPlayerControls(true);
        PlayerManager.Instance.GetPlayerInventory().FoodInStorage = null;
        StoredFoodUI.Instance.UpdateStoreFoodUI(true);
    }
}
