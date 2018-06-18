using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeRegionHandler : MonoBehaviour {
    //Row1

    public delegate void Edit();
    public static event Edit WhenEditPressed;

    public delegate void Insert();
    public static event Insert WhenInsertPressed;

    public delegate void Alter();
    public static event Alter WhenAlterPressed;

    public delegate void Delete();
    public static event Delete WhenDeletePressed;

    public delegate void Undo();
    public static event Undo WhenUndoPressed;


    //Row2

    public delegate void Mem();
    public static event Mem WhenMemPressed;

    public delegate void SingleBlock();
    public static event SingleBlock WhenSingleBlockPressed;

    public delegate void DryRun();
    public static event DryRun WhenDryRunPressed;

    public delegate void OptStop();
    public static event OptStop WhenOptStopPressed;

    public delegate void BlockDelete();
    public static event BlockDelete WhenBlockDeletePressed;

    //Row3

    public delegate void MdiDnc();
    public static event MdiDnc WhenMdiDncPressed;

    public delegate void Coolnt();
    public static event Coolnt WhenCoolntPressed;

    public delegate void SpindleJog();
    public static event SpindleJog WhenSpindleJogPressed;

    public delegate void TurretFwd();
    public static event TurretFwd WhenTurretFwdPressed;

    public delegate void TurretRev();
    public static event TurretRev WhenTurretRevPressed;


    //Row4
    public delegate void HandleJog();
    public static event HandleJog WhenHandleJogPressed;

    public delegate void Point0001();
    public static event Point0001 WhenPoint0001Pressed;

    public delegate void Point001();
    public static event Point001 WhenPoint001Pressed;

    public delegate void Point01();
    public static event Point01 WhenPoint01Pressed;

    public delegate void Point1();
    public static event Point1 WhenPoint1Pressed;

    //Row5

    public delegate void ZeroRet();
    public static event ZeroRet WhenZeroRetPressed;

    public delegate void All();
    public static event All WhenAllPressed;

    public delegate void Origin();
    public static event Origin WhenOriginPressed;

    public delegate void Singl();
    public static event Singl WhenSinglPressed;

    public delegate void HomeG28();
    public static event HomeG28 WhenHomeG28Pressed;

     //Row6

    public delegate void ListProg();
    public static event ListProg WhenListProgPressed;

    public delegate void SelectProg();
    public static event SelectProg WhenSelectProgPressed;

    public delegate void Send();
    public static event Send WhenSendPressed;

    public delegate void Recv();
    public static event Recv WhenRecvPressed;

    public delegate void EraseProg();
    public static event EraseProg WhenEraseProgPressed;

    public void HandleButtonByName(string ButtonName)
    {
        Debug.Log("Handling Button By name " + ButtonName);

        switch (ButtonName)
        {   //Row1
            case "Edit":
                HandleEdit();
                break;
            case "Insert":
                HandleInsert();
                break;
            case "Alter":
                HandleAlter();
                break;
            case "Delete":
                HandleDelete();
                break;
            case "Undo":
                HandleUndo();
                break;
            //Row2
            case "Mem":
                HandleMem();
                break;
            case "SingleBlock":
                HandleSingleBlock();
                break;
            case "DryRun":
                HandleDryRun();
                break;
            case "OptStop":
                HandleOptStop();
                break;
            case "BlockDelete":
                HandleBlockDelete();
                break;
            //Row3
            case "MdiDnc":
                HandleMdiDnc();
                break;
            case "Coolnt":
                HandleCoolnt();
                break;
            case "SpindleJog":
                HandleSpindleJog();
                break;
            case "TurretFwd":
                HandleTurretFwd();
                break;
            case "TurretRev":
                HandleTurretRev();
                break;
            //Row4
            case "HandleJog":
                HandleHandleJog();
                break;
            case "Point0001":
                HandlePoint0001();
                break;
            case "Point001":
                HandlePoint001();
                break;
            case "Point01":
                HandlePoint01();
                break;
            case "Point1":
                HandlePoint1();
                break;
            //Row5
            case "ZeroRet":
                HandleZeroRet();
                break;
            case "All":
                HandleAll();
                break;
            case "Origin":
                HandleOrigin();
                break;
            case "Singl":
                HandleSingl();
                break;
            case "HomeG28":
                HandleHomeG28();
                break;
            //Row6
            case "ListProg":
                HandleListProg();
                break;
            case "SelectProg":
                HandleSelectProg();
                break;
            case "Send":
                HandleSend();
                break;
            case "Recv":
                HandleRecv();
                break;
            case "EraseProg":
                HandleEraseProg();
                break;
 
            default:
                Debug.Log(ButtonName + " value not found");
                break;
        }
    }

    //Row1
    public void HandleEdit()
    {
        if (WhenEditPressed != null) WhenEditPressed();
    }
    public void HandleInsert()
    {
        if (WhenInsertPressed != null) WhenInsertPressed();
    }
    public void HandleAlter()
    {
        if (WhenAlterPressed != null) WhenAlterPressed();
    }
    public void HandleDelete()
    {
        if (WhenDeletePressed != null) WhenDeletePressed();
    }
    public void HandleUndo()
    {
        if (WhenUndoPressed != null) WhenUndoPressed();
    }
    public void HandleMem()
    {
        if (WhenMemPressed != null) WhenMemPressed();
    }
    public void HandleSingleBlock()
    {
        if (WhenSingleBlockPressed != null) WhenSingleBlockPressed();
    }
    public void HandleDryRun()
    {
        if (WhenDryRunPressed != null) WhenDryRunPressed();
    }
    public void HandleOptStop()
    {
        if (WhenOptStopPressed != null) WhenOptStopPressed();
    }
    public void HandleBlockDelete()
    {
        if (WhenBlockDeletePressed != null) WhenBlockDeletePressed();
    }
    public void HandleMdiDnc()
    {
        if (WhenMdiDncPressed != null) WhenMdiDncPressed();
    }
    public void HandleCoolnt()
    {
        if (WhenCoolntPressed != null) WhenCoolntPressed();
    }
    public void  HandleSpindleJog()
    {
        if (WhenSpindleJogPressed != null) WhenSpindleJogPressed();
    }
    public void  HandleTurretFwd()
    {
        if (WhenTurretFwdPressed != null) WhenTurretFwdPressed();
    }
    public void  HandleTurretRev()
    {
        if (WhenTurretRevPressed != null) WhenTurretRevPressed();
    }
    public void  HandleHandleJog()
    {
        if (WhenHandleJogPressed != null) WhenHandleJogPressed();
    }
    public void  HandlePoint0001()
    {
        if (WhenPoint0001Pressed != null) WhenPoint0001Pressed();
    }
    public void  HandlePoint001()
    {
        if (WhenPoint001Pressed != null) WhenPoint001Pressed();
    }
    public void  HandlePoint01()
    {
        if (WhenPoint01Pressed != null) WhenPoint01Pressed();
    }
    public void  HandlePoint1()
    {
        if (WhenPoint1Pressed != null) WhenPoint1Pressed();
    }
    //Row5                  
    public void  HandleZeroRet()
    {
        if (WhenZeroRetPressed != null) WhenZeroRetPressed();
    }
    public void  HandleAll()
    {
        if (WhenAllPressed != null) WhenAllPressed();
    }
    public void  HandleOrigin()
    {
        if (WhenOriginPressed != null) WhenOriginPressed();
    }
    public void  HandleSingl()
    {
        if (WhenSinglPressed != null) WhenSinglPressed();
    }
    public void  HandleHomeG28()
    {
        if (WhenHomeG28Pressed != null) WhenHomeG28Pressed();
    }
    public void  HandleListProg()
    {
        if (WhenListProgPressed != null) WhenListProgPressed();
    }
    public void  HandleSelectProg()
    {
        if (WhenSelectProgPressed != null) WhenSelectProgPressed();
    }
    public void  HandleSend()
    {
        if (WhenSendPressed != null) WhenSendPressed();
    } 
    public void  HandleRecv()
    {
        if (WhenRecvPressed != null) WhenRecvPressed();
    }
    public void  HandleEraseProg()
    {
        if (WhenEraseProgPressed != null) WhenEraseProgPressed();
    }



                
}
