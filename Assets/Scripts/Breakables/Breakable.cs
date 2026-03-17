using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private PlayerChecker playerChecker;
    [SerializeField] private GameObject breakKeyObject;
    [SerializeField] private KeyCode breakKey;

    private void Update()
    {
        if (playerChecker.PlayerIsNear)
        {
            if (BreakableManager.Instance.NearbyBreakables[0] == this)
            {
                breakKeyObject.SetActive(true);
                if (Input.GetKeyDown(breakKey))
                {
                    Break();
                }
            }
        }
        else
        {
            breakKeyObject.SetActive(false);
        }
    }

    public void Break()
    {
        // break anim

        // spawn shit

        Destroy(gameObject);
    }
}
