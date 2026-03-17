using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableManager : MonoBehaviour
{
    private static BreakableManager instance;
    public static BreakableManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newObj = new GameObject("BreakableManager");
                instance = newObj.AddComponent<BreakableManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private List<Breakable> nearbyBreakables = new List<Breakable>();
    public List<Breakable> NearbyBreakables
    {
        get => nearbyBreakables;
    }
}
