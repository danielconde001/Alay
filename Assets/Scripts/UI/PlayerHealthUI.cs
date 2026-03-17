using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image staminaBarFill;
    [SerializeField] private Image healthBarFill;

    private Health playerHealth;
    private PlayerStamina playerStamina;

    private void Start()
    {
        playerHealth = PlayerManager.Instance.GetPlayer().GetComponent<Health>();
        playerStamina = PlayerManager.Instance.GetPlayer().GetComponent<PlayerStamina>();
    }

    public void UpdateStaminaBar()
    {
        staminaBarFill.fillAmount = playerStamina.CurrentStaminaPoints / playerStamina.MaxStaminaPoints;
    }

    public void UpdateHealthBar()
    {
        healthBarFill.fillAmount = (float)playerHealth.CurrentHealthPoints / (float)playerHealth.MaxHealthPoints;
    }
}
