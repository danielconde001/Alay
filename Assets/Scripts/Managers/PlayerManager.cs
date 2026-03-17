using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newObj = new GameObject("PlayerManager");
                instance = newObj.AddComponent<PlayerManager>();
            }

            return instance;
        }
    }

    private GameObject player;

    private PlayerInventory playerInventory;

    private Cainos.PixelArtTopDown_Basic.TopDownCharacterController characterController;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");

        playerInventory = new PlayerInventory();

        characterController = player?.GetComponent<Cainos.PixelArtTopDown_Basic.TopDownCharacterController>();
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public PlayerInventory GetPlayerInventory()
    {
        return playerInventory;
    }

    public Cainos.PixelArtTopDown_Basic.TopDownCharacterController GetCharacterController()
    {
        return characterController;
    }

    public void SetPlayerControls(bool p_value)
    {
        if (characterController != null)
        {
            GetPlayer().GetComponent<Rigidbody2D>().simulated = p_value;
            characterController.enabled = p_value;
        }
    }
}

