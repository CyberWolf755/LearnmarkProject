using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour {

    [HideInInspector]
    public ToolHolder AttachedToolHolder;
    [HideInInspector]
    public CuttingTool AttachedCuttingTool;

    public bool ToolHolderTightened = false;
    public bool ToolHolderConnected = false;
    public bool CuttingToolTightened = false;
    public bool CuttingToolConnected = false;



    [SerializeField]
    private ToolPieceConnectionPoint toolPieceConPoint1;
    [SerializeField]
    private ToolPieceConnectionPoint toolPieceConPoint2;



    private void OnEnable()
    {
        toolPieceConPoint1.OnObjectConnected += HandleTool1Connected;
        toolPieceConPoint2.OnObjectConnected += HandleTool2Connected;

        toolPieceConPoint1.OnObjectDisconnected += HandleTool1Disconnected;
        toolPieceConPoint2.OnObjectDisconnected += HandleTool2Disconnected;
    }

    private void OnDisable()
    {
        toolPieceConPoint1.OnObjectConnected -= HandleTool1Connected;
        toolPieceConPoint2.OnObjectConnected -= HandleTool2Connected;

        toolPieceConPoint1.OnObjectDisconnected -= HandleTool1Disconnected;
        toolPieceConPoint2.OnObjectDisconnected -= HandleTool2Disconnected;
    }

    public void SetCuttingTool(CuttingTool cuttingTool)
    {
        AttachedCuttingTool = cuttingTool;
    }

    public void SetToolPiece(ToolHolder toolPiece)
    {
        AttachedToolHolder = toolPiece;
    }
    public void SetToolPieceTightened(bool state)
    {
        ToolHolderTightened = state;
    }
    public void SetCuttingToolTightened(bool state)
    {
        CuttingToolTightened = state;
    }


    public void HandleTool1Connected(ConnectableTool connectedTool)
    {
       if(connectedTool.GetComponent<CuttingTool>() != null)
        {
            //Cutting tool can only be connected if the tool Holder is already attached
            
                AttachedCuttingTool = connectedTool.GetComponent<CuttingTool>();
                CuttingToolConnected = true;
           
        }
       if(connectedTool.GetComponent<ToolHolder>() != null)
        {
            AttachedToolHolder = connectedTool.GetComponent<ToolHolder>();
            AttachedToolHolder.OnToolHolderTightened += HandleOnToolHolderTightened;
            AttachedToolHolder.OnToolHolderUnTightened += HandleOnToolHolderUnTightened;

            ToolHolderConnected = true;
        }

        toolPieceConPoint2.gameObject.SetActive(true);


    }
    public void HandleTool2Connected(ConnectableTool connectedTool)
    {

        if (connectedTool.GetComponent<CuttingTool>() != null)
        {
           
            
                AttachedCuttingTool = connectedTool.GetComponent<CuttingTool>();
                CuttingToolConnected = true;
            
           /* //Otherwise disconnect it
            if (ToolHolderConnected != true)
            {
                toolPieceConPoint2.ForceDisconnectObject();
            }*/
        }
        if (connectedTool.GetComponent<ToolHolder>() != null)
        {
            AttachedToolHolder = connectedTool.GetComponent<ToolHolder>();
            AttachedToolHolder.OnToolHolderTightened += HandleOnToolHolderTightened;
            AttachedToolHolder.OnToolHolderUnTightened += HandleOnToolHolderUnTightened;

            ToolHolderConnected = true;
        }

    }

    public void HandleTool1Disconnected(ConnectableTool connectedTool)
    {

        if (connectedTool.GetComponent<CuttingTool>() != null)
        {
            AttachedCuttingTool = null;
            CuttingToolConnected = false;
        }
        if (connectedTool.GetComponent<ToolHolder>() != null)
        {
            AttachedToolHolder.OnToolHolderTightened -= HandleOnToolHolderTightened;
            AttachedToolHolder = null;
            ToolHolderConnected = false;

            if(CuttingToolConnected == true)
            {
                toolPieceConPoint2.ForceDisconnectObject();
                CuttingToolConnected = false;
            }

        }
        toolPieceConPoint2.gameObject.SetActive(false);

    }

    public void HandleTool2Disconnected(ConnectableTool connectedTool)
    {
        if (connectedTool.GetComponent<CuttingTool>() != null)
        {
            AttachedCuttingTool = null;
            CuttingToolConnected = false;
        }
        if (connectedTool.GetComponent<ToolHolder>() != null)
        {
            AttachedToolHolder.OnToolHolderTightened -= HandleOnToolHolderTightened;
            AttachedToolHolder = null;
            ToolHolderConnected = false;


        }

    }

    public void HandleOnToolHolderTightened()
    {
        Debug.Log("Tool holder has been tightened");
        ToolHolderTightened = true;
        AttachedToolHolder.GetComponent<ConnectableTool>().IsInteractable = false;
        if (AttachedCuttingTool != null)
        {
            CuttingToolTightened = true;
            AttachedCuttingTool.GetComponent<ConnectableTool>().IsInteractable = false;
        }
        if(AttachedCuttingTool == null)
        {
            toolPieceConPoint2.gameObject.SetActive(false);
        }
    }

    public void HandleOnToolHolderUnTightened()
    {
        Debug.Log("Tool holder has been untightened");
        ToolHolderTightened = false;
        AttachedToolHolder.GetComponent<ConnectableTool>().IsInteractable = true;
        if (AttachedCuttingTool != null)
        {
            CuttingToolTightened = false;
            AttachedCuttingTool.GetComponent<ConnectableTool>().IsInteractable = true;
        }
        if (AttachedCuttingTool == null && toolPieceConPoint2.gameObject.activeSelf == false)
        {
            toolPieceConPoint2.gameObject.SetActive(true);
        }
    }

    public void ForceDisconnectUntightened()
    {
        if(ToolHolderConnected == true && CuttingToolConnected == true)
        {
            if(ToolHolderTightened != true)
            {
                toolPieceConPoint2.ForceDisconnectObject();
                CuttingToolTightened = false;
                CuttingToolConnected = false;
            }
        }
    }

}
