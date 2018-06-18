using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHolder : MonoBehaviour {

    [HideInInspector]
    public bool IsConnected = false;
    private bool IsBigScrew1Connected = false;
    private bool IsBigScrew2Connected = false;
    private bool IsSmallScrewConnected = false;
    private bool IsBigScrew1Tightened = false;
    private bool IsBigScrew2Tightened = false;
    private bool IsSmallScrewTightened = false;
    private bool IsTightened = false;
    [HideInInspector]
    public ToolManager toolManager;


    [SerializeField]
    private ToolPieceConnectionPoint bigScrew1ConnectionPoint;
    [SerializeField]
    private ToolPieceConnectionPoint bigScrew2ConnectionPoint;
    [SerializeField]
    private ToolPieceConnectionPoint smallScrewConnectionPoint;

    // Callback signature
    public delegate void ToolHolderTightened();
    // event declaration
    public event ToolHolderTightened OnToolHolderTightened;

    // Callback signature
    public delegate void ToolHolderUnTightened();
    // event declaration
    public event ToolHolderUnTightened OnToolHolderUnTightened;

    //Event Subscription(Connect/Disconnect)
    private void OnEnable()
    {
        bigScrew1ConnectionPoint.OnObjectConnected += HandleBigScrew1Connected;
        bigScrew2ConnectionPoint.OnObjectConnected += HandleBigScrew2Connected;
        smallScrewConnectionPoint.OnObjectConnected += HandleSmallScrewConnected;

        bigScrew1ConnectionPoint.OnObjectDisconnected += HandleBigScrew1Disconnected;
        bigScrew2ConnectionPoint.OnObjectDisconnected += HandleBigScrew2Disconnected;
        smallScrewConnectionPoint.OnObjectDisconnected += HandleSmallScrewDisconnected;
    }
    private void OnDisable()
    {
        bigScrew1ConnectionPoint.OnObjectConnected -= HandleBigScrew1Connected;
        bigScrew2ConnectionPoint.OnObjectConnected -= HandleBigScrew2Connected;
        smallScrewConnectionPoint.OnObjectConnected -= HandleSmallScrewConnected;

        bigScrew1ConnectionPoint.OnObjectDisconnected -= HandleBigScrew1Disconnected;
        bigScrew2ConnectionPoint.OnObjectDisconnected -= HandleBigScrew2Disconnected;
        smallScrewConnectionPoint.OnObjectDisconnected -= HandleSmallScrewDisconnected;
    }


    private void HandleBigScrew1Connected(ConnectableTool connectedTool)
    {
        IsBigScrew1Connected = true;
        Screw screwScript = connectedTool.GetComponent<Screw>();
        screwScript.SetKeyConnectionPointActive();
        screwScript.OnScrewTightened += HandleScrew1Tightened;
        screwScript.OnScrewUntightened += HandleScrew1Untightened;
        IsBigScrew1Tightened = false;

    }

    private void HandleBigScrew2Connected(ConnectableTool connectedTool)
    {
        IsBigScrew2Connected = true;
        Screw screwScript = connectedTool.GetComponent<Screw>();
        screwScript.SetKeyConnectionPointActive();
        screwScript.OnScrewTightened += HandleScrew2Tightened;
        screwScript.OnScrewUntightened += HandleScrew2Untightened;
        IsBigScrew2Tightened = false;
    }

    private void HandleSmallScrewConnected(ConnectableTool connectedTool)
    {
        IsSmallScrewConnected = true;
        Screw screwScript = connectedTool.GetComponent<Screw>();
        screwScript.SetKeyConnectionPointActive();
        screwScript.OnScrewTightened += HandleSmallScrewTightened;
        screwScript.OnScrewUntightened += HandleSmallScrewUntightened;
        IsSmallScrewTightened = false;
    }

    private void HandleBigScrew1Disconnected(ConnectableTool connectedTool)
    {
        IsBigScrew1Connected = false;
        Screw screwScript = connectedTool.GetComponent<Screw>();
        screwScript.SetKeyConnectionPointInactive();
        screwScript.OnScrewTightened -= HandleScrew1Tightened;
        screwScript.OnScrewUntightened -= HandleScrew1Untightened;
        IsBigScrew1Tightened = false;
        if (IsTightened == true)
        {
            IsTightened = false;
            if (OnToolHolderUnTightened != null) OnToolHolderUnTightened.Invoke();
        }

    }

    private void HandleBigScrew2Disconnected(ConnectableTool connectedTool)
    {
        IsBigScrew2Connected = false;
        Screw screwScript = connectedTool.GetComponent<Screw>();
        screwScript.SetKeyConnectionPointInactive();
        screwScript.OnScrewTightened -= HandleScrew2Tightened;
        screwScript.OnScrewUntightened -= HandleScrew2Untightened;
        IsBigScrew2Tightened = false;
        if (IsTightened == true)
        {
            IsTightened = false;
            if (OnToolHolderUnTightened != null) OnToolHolderUnTightened.Invoke();
        }
    }

    private void HandleSmallScrewDisconnected(ConnectableTool connectedTool)
    {
        IsSmallScrewConnected = false;
        Screw screwScript = connectedTool.GetComponent<Screw>();
        screwScript.SetKeyConnectionPointInactive();
        screwScript.OnScrewTightened -= HandleSmallScrewTightened;
        screwScript.OnScrewUntightened -= HandleSmallScrewUntightened;
        IsSmallScrewTightened = false;
        if(IsTightened == true)
        {
            IsTightened = false;
            if (OnToolHolderUnTightened != null) OnToolHolderUnTightened.Invoke();
        }
    }

   

    private void CheckIfEverythingTightened()
    {

        if(IsBigScrew1Tightened == true && IsBigScrew2Tightened == true && IsSmallScrewTightened == true)
        {
            IsTightened = true;
            if (OnToolHolderTightened != null)
            {
                OnToolHolderTightened.Invoke();
                Debug.Log("Sending is tightened event");
            }
        }
        else if(IsTightened == true)
        {

            IsTightened = false;
            if (OnToolHolderUnTightened != null)
            {
                OnToolHolderUnTightened.Invoke();
                Debug.Log("Sending is untightened event");
            }
        }
        
    }

    public void SetToolManager(ToolManager manager)
    {
        toolManager = manager;
        toolManager.SetToolPiece(this);
    }

    private void HandleScrew1Tightened()
    {
        IsBigScrew1Tightened = true;
        CheckIfEverythingTightened();
    }
    private void HandleScrew2Tightened()
    {
        IsBigScrew2Tightened = true;
        CheckIfEverythingTightened();
    }
    private void HandleSmallScrewTightened()
    {
        IsSmallScrewTightened = true;
        CheckIfEverythingTightened();
    }
    private void HandleScrew1Untightened()
    {
        IsBigScrew1Tightened = false;
        CheckIfEverythingTightened();
    }
    private void HandleScrew2Untightened()
    {
        IsBigScrew2Tightened = false;
        CheckIfEverythingTightened();
    }
    private void HandleSmallScrewUntightened()
    {
        IsSmallScrewTightened = false;
        CheckIfEverythingTightened();
    }
}
