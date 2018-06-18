using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTool : MonoBehaviour {

    [HideInInspector]
    public bool IsConnected = false;
    [HideInInspector]
    public bool IsTightened = false;
    [HideInInspector]
    public ToolManager toolManager;

    public void ToolConnected(bool state)
    {
        IsConnected = state;
    }

    public void ToolTightened(bool state)
    { 
        IsTightened = state;
    }

    public void SetToolManager(ToolManager manager)
    {
        toolManager = manager;
        toolManager.SetCuttingTool(this);
    }

    
}
