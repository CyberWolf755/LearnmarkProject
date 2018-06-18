using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineDisplayManager : MonoBehaviour
{

    //TODO - When machine turns on, the initial screen is the alarm screen
    //Alarm sccreen is turned on,
    [SerializeField]
    private StateMachine stateMachine;

    /* public GameObject displayScreen;*/

   // public GameObject displayScreenCanvas;

        //Active screens
    public GameObject StartingUpScreen;
    public GameObject ParentScreen;

    //Current mode region
    public GameObject InitialMode;
    public GameObject ListProgramsMode;

    //Program Display Region
    public GameObject InitialProgramDisplayWindow;
    public GameObject ChangedProgramDisplayWindow;
    public GameObject ListProgProgramDisplayWindow;


    //Offset windows in the Main Display Pane Region
    public GameObject OffsetRegionWindows;
    public GameObject CurrentCommandsWindows;
    //Messages for main display Pane
    public GameObject MessageScreen;
    public GameObject ServosOffMessage;
    public GameObject EmergencyOffMessage;
    public GameObject NoAlarmsMessage;
    //List Progt windows for Main Display Pane Region
    public GameObject ListProgWithoutUSBDisplayParent;
    public GameObject ListProgDisplayParent;
    public GameObject ListProgMemoryWindow;
    public GameObject ListProgUSBDeviceWindow;

    public GameObject SpeedAndFeedStatusRegion;
    public GameObject EditorHelpRegion;

    public GameObject PositionDisplayRegion;
    public GameObject AxisLoadMetersRegion;
    public GameObject ClipboardRegion;

    
    public GameObject TimersCountersRegion;
    public GameObject ToolMgmtRegion;

    //Alarm region display window messages
    public GameObject AlarmRegionServosOffMessage;
    public GameObject AlarmRegionEmergencyOffMessage;
    public GameObject AlarmRegionCycleDoorMessage;

    public GameObject InputRegion;


    [SerializeField]
    private float delayTime = 2f;

    public void MachineTurnedOn()
    {
        StartCoroutine(LateActivateStartupScreen());
    }

    IEnumerator LateActivateStartupScreen()
    {

        yield return new WaitForSeconds(delayTime);

        StartingUpScreen.SetActive(true);

        StartCoroutine(LateDeActivateStartupScreen());

    }

    IEnumerator LateDeActivateStartupScreen()
    {

        yield return new WaitForSeconds(delayTime);

        StartingUpScreen.SetActive(false);
        ParentScreen.SetActive(true);
        ServosOffMessage.SetActive(true);
        InitialProgramDisplayWindow.SetActive(true);
       
        if (stateMachine.emergencyOffAlarm == true) EmergencyOffMessage.SetActive(true);


        ActivateInitialScreenState();

    }

    private void ActivateInitialScreenState()
    {
        InitialMode.SetActive(true);
        MessageScreen.SetActive(true);
        SpeedAndFeedStatusRegion.SetActive(true);
        PositionDisplayRegion.SetActive(true);
        TimersCountersRegion.SetActive(true);
    }
    private void DeActivateInitialScreenState()
    {
        InitialMode.SetActive(false);
        MessageScreen.SetActive(false);
        SpeedAndFeedStatusRegion.SetActive(false);
        PositionDisplayRegion.SetActive(false);
        TimersCountersRegion.SetActive(false);
    }

    private void ActivateListProgramsScreenState()
    {
        if (stateMachine.UsbConnected == true)
        {
            ListProgramsMode.SetActive(true);
            ListProgProgramDisplayWindow.SetActive(true);
            ListProgDisplayParent.SetActive(true);
            EditorHelpRegion.SetActive(true);
            ClipboardRegion.SetActive(true);
        }
      

        if(stateMachine.UsbConnected == false)
        {
            ListProgramsMode.SetActive(true);
            ListProgProgramDisplayWindow.SetActive(true);
            ListProgWithoutUSBDisplayParent.SetActive(true);
            EditorHelpRegion.SetActive(true);
            ClipboardRegion.SetActive(true);
        }
    }

    private void DeActivateListProgramsScreenState()
    {
        
            ListProgramsMode.SetActive(false);
            ListProgProgramDisplayWindow.SetActive(false);
            ListProgDisplayParent.SetActive(false);
            ListProgWithoutUSBDisplayParent.SetActive(false);
            EditorHelpRegion.SetActive(false);
            ClipboardRegion.SetActive(false);
        
     
    }


    public void MachineTurnedOff()
    {
        ParentScreen.SetActive(false);
        EmergencyOffMessage.SetActive(false);
        ServosOffMessage.SetActive(false);
        OffsetRegionWindows.SetActive(false);
        CurrentCommandsWindows.SetActive(false);
        DeActivateInitialScreenState();
        DeActivateListProgramsScreenState();

        // displayScreenCanvas.SetActive(false);
    }

    public void TurnOffAlarm(bool emergencyOffAlarm, bool servosOffAlarm)
    {
        if (emergencyOffAlarm == true)
        {
            if(EmergencyOffMessage.activeSelf == true)
            {
                EmergencyOffMessage.SetActive(false);
                AlarmRegionEmergencyOffMessage.SetActive(false);
            }
        }
        if (servosOffAlarm == true)
        {
            if (ServosOffMessage.activeSelf == true)
            {
                ServosOffMessage.SetActive(false);
                AlarmRegionServosOffMessage.SetActive(false);
            }
        }

        if(EmergencyOffMessage.activeSelf == false && ServosOffMessage.activeSelf == false)
        {
            NoAlarmsMessage.SetActive(true);
        }

    }

    public void TurnOnAlarm(bool emergencyOffAlarm, bool servosOffAlarm)
    {
        if (emergencyOffAlarm)
        {
            NoAlarmsMessage.SetActive(false);

            EmergencyOffMessage.SetActive(true); AlarmRegionEmergencyOffMessage.SetActive(true);
           
        }
        else if (servosOffAlarm)
        {
            NoAlarmsMessage.SetActive(false);

            ServosOffMessage.SetActive(true); AlarmRegionServosOffMessage.SetActive(true);

        }
    }

    public void HandleIsAnyoneThere(bool isAnyoneThere)
    {
        AlarmRegionCycleDoorMessage.SetActive(isAnyoneThere);
    }

    public void HandleOffsetWindowState(bool state)
    {
        OffsetRegionWindows.SetActive(state);

        CurrentCommandsWindows.SetActive(false);
        MessageScreen.SetActive(false);
    }

    public void HandleCurrentCommandsWindowState(bool state)
    {
        CurrentCommandsWindows.SetActive(state);

        MessageScreen.SetActive(false);
        OffsetRegionWindows.SetActive(false);

    }


    public void HandleHomeScreen()
    {
        DeActivateListProgramsScreenState();
        ActivateInitialScreenState();

        OffsetRegionWindows.SetActive(false);
        CurrentCommandsWindows.SetActive(false);
    }



    public void HandleListProgScreen(bool state)
    {
        if(state == true)
        {
            ActivateListProgramsScreenState();
            ListProgProgramDisplayWindow.SetActive(true);
            DeActivateInitialScreenState();
            InitialProgramDisplayWindow.SetActive(false);
        }
        else
        {
            DeActivateListProgramsScreenState();
            ActivateInitialScreenState();
        }
    }

    public void HandleListProgWithoutUSB()
    {

    }

    public void HandleLeftCursor()
    {
       if(ListProgDisplayParent.activeSelf == true)
        {
            ListProgMemoryWindow.SetActive(true);
            ListProgUSBDeviceWindow.SetActive(false);

        }
    }

    public void HandleUpCursor()
    {
        throw new NotImplementedException();
    }

    public void HandleRightCursor()
    {
        if (ListProgDisplayParent.activeSelf == true)
        {
            ListProgMemoryWindow.SetActive(false);
            ListProgUSBDeviceWindow.SetActive(true);

        }
    }

    internal void HandleDownCursor()
    {
        
    }

    public void HandleSelectProgramPressed()
    {
        if (ListProgDisplayParent.activeSelf == true)
        {
            if(ListProgUSBDeviceWindow.activeSelf == true)
            {
                InitialProgramDisplayWindow.SetActive(false);
                ListProgProgramDisplayWindow.SetActive(false);
                ChangedProgramDisplayWindow.SetActive(true);

            }

        }

    }
}
