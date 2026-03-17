using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FlowchartManager : MonoBehaviour
{
    private static FlowchartManager instance;
    public static FlowchartManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newObj = new GameObject("FlowchartManager");
                instance = newObj.AddComponent<FlowchartManager>();
            }

            return instance;
        }
    }

    private Flowchart flowchart;

    private void Awake()
    {
        instance = this;
        flowchart = FindObjectOfType<Flowchart>();
    }

    public Flowchart GetMainFlowchart()
    {
        return flowchart;
    }
}
