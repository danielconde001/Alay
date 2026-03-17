using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsManager : MonoBehaviour
{
    private static BuffsManager instance;
    public static BuffsManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newObj = new GameObject("BuffsManager");
                instance = newObj.AddComponent<BuffsManager>();
            }

            return instance;
        }
    }

    private List<int> appliedBuffs = new List<int>();
    public List<int> AppliedBuffs
    {
        get => appliedBuffs;
    }

    private void Awake()
    {
        instance = this;
    }

    public void ApplyBuff(int p_ID)
    {
        switch (p_ID)
        {
            case 0:
                ApplySpreadShot();
                break;
            case 1:
                ApplyExplosiveShot();
                break;
            case 2:
                ApplyFireRateBoost();
                break;
        }
    }

    private void ApplySpreadShot()
    {
        PlayerManager.Instance.GetCharacterController().CanSpread = true;
        PlayerManager.Instance.GetCharacterController().NumberOfProjectilesInSpread += 2;
    }

    private void ApplyExplosiveShot()
    {
        PlayerManager.Instance.GetCharacterController().HasProjectileBuff = true;
        PlayerManager.Instance.GetCharacterController().MinDamage += 10;
        PlayerManager.Instance.GetCharacterController().MinDamage += 10;
    }

    private void ApplyFireRateBoost()
    {
        PlayerManager.Instance.GetCharacterController().FireRate /= 4;
    }
}
