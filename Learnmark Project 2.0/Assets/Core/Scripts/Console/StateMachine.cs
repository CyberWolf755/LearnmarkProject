using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    public ToolManager CurrentToolSlot;

    //Key states for handling events
    public bool powerOn { get; set; }
    private bool emergencyStop = true;
    private bool cycleStart = false;
    public bool doorClosed { get; set; }
    public bool UsbConnected { get; set; }
    public bool CodeReady { get; set; }
    public bool MaterialConnected { get; set; }

    //Alarm fields
    private bool alarmState = false;
    public bool emergencyOffAlarm { get; set; }
    private bool servosOffAlarm = false;
    private bool isAnyoneThere = false; //HELOOOO. Is there anyone here?

    //Upper right window states
    private bool currentCommandsWindows = false;
    private bool offetWindows = false;
    private bool messageWindow = false;


    //Tool states
    private bool cuttingToolConnected = false;
    private bool toolHolderConnected = false;
    private bool cuttingToolTightened = false;
    private bool toolHolderTightened = false;

    [SerializeField]
    private StateMachineDisplayManager displayManager;


    //TODO - Move these into another script
    public float TimeToMakeObject;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip MachineIdleAudioClip;
    [SerializeField] AudioClip MachineCuttingAudioClip;

    IEnumerator co;

    public ToolPieceCuttingAnimation CuttingAnimation;

   

    private void Awake()
    { //Subscribe to LEFT AREA
        powerOn = false;
        emergencyOffAlarm = false;
        LeftSideRegionHandler.WhenPoweredOn += HandlePoweredOn;
        LeftSideRegionHandler.WhenPoweredOff += HandlePoweredOff;
        LeftSideRegionHandler.WhenEmergencyStopped += HandleEmergencyStopped;
        LeftSideRegionHandler.WhenCycleStarted += HandleCycleStarted;
        LeftSideRegionHandler.WhenCycleStopped += HandleCycleStopped;
    //
        co = StartMakingObject(); //Coroutine subscription

        //subscribe to FUNCTION REGION
        FunctionRegionHandler.WhenResetPressed += HandleResetPressed;
        FunctionRegionHandler.WhenPowerUpPressed += HandleResetPressed;
        FunctionRegionHandler.WhenRecoverPressed += HandlePowerUpPressed;
        FunctionRegionHandler.WhenF1Pressed += HandleF1Pressed;
        FunctionRegionHandler.WhenF2Pressed += HandleF2Pressed;
        FunctionRegionHandler.WhenF3Pressed += HandleF3Pressed;
        FunctionRegionHandler.WhenF4Pressed += HandleF4Pressed;
        FunctionRegionHandler.WhenToolOffsetMeasurePressed += HandleToolOffsetMeasurePressed;
        FunctionRegionHandler.WhenNextToolPressed += HandleNextToolPressed;
        FunctionRegionHandler.WhenToolReleasePressed += HandleToolReleasedPressed;
        FunctionRegionHandler.WhenPointZeroSetPressed += HandlePointZeroSetPressed;

        //Subscribe to DISPLAY REGION

        DisplayRegionHandler.WhenProgramConversionPressed += HandleProgramConversionPressed;
        DisplayRegionHandler.WhenPositionPressed += HandlePositionPressed;
        DisplayRegionHandler.WhenOffsetPressed += HandleOffsetPressed;
        DisplayRegionHandler.WhenCurrentCommandsPressed += HandleCurrentCommandsPressed;
        DisplayRegionHandler.WhenAlarmMessagesPressed += HandleAlarmMessagesPressed;
        DisplayRegionHandler.WhenParameterDiagnosisPressed += HandleParameterDiagnosisPressed;
        DisplayRegionHandler.WhenSettingsGraphPressed += HandleSettingsGraphPressed;
        DisplayRegionHandler.WhenHelpCalculationPressed += HandleHelpCalculationPressed;

        //Subscribe to CURSOR REGION

        CursorRegionHandler.WhenHomePressed += HandleHomePressed;
        CursorRegionHandler.WhenUpPressed += HandleUpPressed;
        CursorRegionHandler.WhenPageUpPressed += HandlePageUpPressed;
        CursorRegionHandler.WhenLeftPressed += HandleLeftPressed;
        CursorRegionHandler.WhenRightPressed += HandleRightPressed;
        CursorRegionHandler.WhenEndPressed += HandleEndPressed;
        CursorRegionHandler.WhenDownPressed += HandleDownPressed;
        CursorRegionHandler.WhenPageDownPressed += HandlePageDownPressed;

        //Subscribe to MODE REGION

        //Row1
        ModeRegionHandler.WhenEditPressed += HandleEditPressed;
        ModeRegionHandler.WhenInsertPressed += HandleInsertPressed;
        ModeRegionHandler.WhenAlterPressed += HandleAlterPressed;
        ModeRegionHandler.WhenDeletePressed += HandleDeletePressed;
        ModeRegionHandler.WhenUndoPressed += HandleUndoPressed;
        //Row2
        ModeRegionHandler.WhenMemPressed += HandleMemPressed;
        ModeRegionHandler.WhenSingleBlockPressed += HandleSingleBlockPressed;
        ModeRegionHandler.WhenDryRunPressed += HandleDryRunPressed;
        ModeRegionHandler.WhenOptStopPressed += HandleOptStopPressed;
        ModeRegionHandler.WhenBlockDeletePressed += HandleBlockDeletePressed;
        //Row3
        ModeRegionHandler.WhenMdiDncPressed += HandleMdiDncPressed;
        ModeRegionHandler.WhenCoolntPressed += HandleCoolntPressed;
        ModeRegionHandler.WhenSpindleJogPressed += HandleSpindleJogPressed;
        ModeRegionHandler.WhenTurretFwdPressed += HandleTurretFwdPressed;
        ModeRegionHandler.WhenTurretRevPressed += HandleTurretRevPressed;
        //Row4
        ModeRegionHandler.WhenHandleJogPressed += HandleHandleJogPressed;
        ModeRegionHandler.WhenPoint0001Pressed += HandlePoint0001Pressed;
        ModeRegionHandler.WhenPoint001Pressed += HandlePoint001Pressed;
        ModeRegionHandler.WhenPoint01Pressed += HandlePoint01Pressed;
        ModeRegionHandler.WhenPoint1Pressed += HandlePoint1Pressed;
        //Row5
        ModeRegionHandler.WhenZeroRetPressed += HandleZeroRetPressed;
        ModeRegionHandler.WhenAllPressed += HandleAllPressed;
        ModeRegionHandler.WhenOriginPressed += HandleOriginPressed;
        ModeRegionHandler.WhenSinglPressed += HandleSinglPressed;
        ModeRegionHandler.WhenHomeG28Pressed += HandleHomeG28Pressed;
        //Row6
        ModeRegionHandler.WhenListProgPressed += HandleListProgPressed;
        ModeRegionHandler.WhenSelectProgPressed += HandleSelectProgPressed;
        ModeRegionHandler.WhenSendPressed += HandleSendPressed;
        ModeRegionHandler.WhenRecvPressed += HandleRecvPressed;
        ModeRegionHandler.WhenEraseProgPressed += HandleEraseProgPressed;

        //Subscribe to NUMBERS REGION
        //row1
        NumericRegionHandler.WhenSevenPressed += HandleSevenPressed;
        NumericRegionHandler.WhenEightPressed += HandleEightPressed;
        NumericRegionHandler.WhenNinePressed += HandleNinePressed;
        //Row 2
        NumericRegionHandler.WhenFourPressed += HandleFourPressed;
        NumericRegionHandler.WhenEightPressed += HandleFivePressed;
        NumericRegionHandler.WhenNinePressed += HandleSixPressed;
        //Row3
        NumericRegionHandler.WhenSevenPressed += HandleOnePressed;
        NumericRegionHandler.WhenEightPressed += HandleTwoPressed;
        NumericRegionHandler.WhenNinePressed += HandleThreePressed;
        //Row4
        NumericRegionHandler.WhenSevenPressed += HandleMinusPressed;
        NumericRegionHandler.WhenEightPressed += HandleZeroPressed;
        NumericRegionHandler.WhenNinePressed += HandleTimesPressed;
        //Row5
        NumericRegionHandler.WhenSevenPressed += HandleCancelPressed;
        NumericRegionHandler.WhenEightPressed += HandleSpacePressed;
        NumericRegionHandler.WhenNinePressed += HandleWritePressed;
    }

    
    // Use this for initialization


    public void HandlePoweredOn()
    {
        if (powerOn == false)
        {
            powerOn = true;
            alarmState = true;
            messageWindow = true;
            servosOffAlarm = true;
            isAnyoneThere = false;
            
            if(emergencyStop)
            {
                emergencyOffAlarm = true;
            }
            displayManager.MachineTurnedOn();

        }


    }
    public void HandlePoweredOff()
    {
      if(powerOn == true)
        {
            powerOn = false;
            cycleStart = false;
            displayManager.MachineTurnedOff();
            if (audioSource.isPlaying == true) audioSource.Stop();

        }

    }
    public void HandleEmergencyStopped()
    {
        emergencyStop = !emergencyStop;

        if (emergencyStop == true)
        {
            cycleStart = false;
            emergencyOffAlarm = true;
            displayManager.TurnOnAlarm(true, false);
            alarmState = true;
            if (audioSource.isPlaying == true) audioSource.Stop();

        }
        if(emergencyStop == false)// If we released the emergency stop button
        {
            
            if(emergencyOffAlarm && !isAnyoneThere) 
            {
                displayManager.HandleIsAnyoneThere(!isAnyoneThere);
            }
        }


    }
    public void HandleCycleStarted()
    {
        if (emergencyStop == false && powerOn == true && doorClosed == true && UsbConnected == true && alarmState == false && CodeReady == true)
        {
            
            cycleStart = true;
            if(audioSource.isPlaying == true)
            {
                audioSource.Stop();
            }

            audioSource.PlayOneShot(MachineCuttingAudioClip);

            
            CuttingAnimation.SetStartAnimation(CurrentToolSlot,MaterialConnected);

              //  MaterialToCut.SetActive(false);
                // Instantiate(ChangedMaterial, MaterialConnectionPoint.transform.position, MaterialConnectionPoint.transform.rotation);
               
                //  StartCoroutine(co);
                Debug.Log("Starting Cycle");
            
                       

        }
    }
    public void HandleCycleStopped()
    {
                    cycleStart = false;
                   // StopCoroutine(co);

                    Debug.Log("Stopping Cycle");
    }

    public void HandleDoorOpened()
    {
        doorClosed = false;
        Debug.Log("Door Opened");
        cycleStart = false;

        if(!isAnyoneThere && !emergencyStop && emergencyOffAlarm)
        {
            isAnyoneThere = true;
            displayManager.HandleIsAnyoneThere(!isAnyoneThere);
        }

        CuttingAnimation.StopAnimation();

      
    }

    public void HandleDoorClosed()
    {
        doorClosed = true;
        Debug.Log("Door Closed");
    }

    private IEnumerator StartMakingObject()
    {
            if(TimeToMakeObject == 0f)
            {
                yield return null;
            }
    }





    public void HandleResetPressed()
    {
        if(powerOn)
        {
            if(alarmState)
            {
                if(emergencyOffAlarm)
                {
                    if(!emergencyStop && isAnyoneThere)
                    {
                        displayManager.TurnOffAlarm(true, false);
                        emergencyOffAlarm = false;
                    }

                }

                else if (servosOffAlarm)
                {
                        displayManager.TurnOffAlarm(false, true);
                    servosOffAlarm = false;
                }

                if (!servosOffAlarm && !emergencyOffAlarm)
                {
                    displayManager.TurnOffAlarm(false, false);
                    alarmState = false;
                    audioSource.Play();
                }
            }
           
        }
    }

    public void HandlePowerUpPressed()
    {
        if (powerOn)
        {

        }
    }
    public void HandleRecoverPressed()
    {
        if (powerOn)
        {

        }
    }
    public void HandleF1Pressed()
    {
        if (powerOn)
        {

        }
    }
    public void HandleF2Pressed()
    {
        if (powerOn)
        {

        }
    }

    public void HandleF3Pressed()
    {
        if (powerOn)
        {

        }
    }

    public void HandleF4Pressed()
    {
        if (powerOn)
        {

        }
    }

    public void HandleToolOffsetMeasurePressed()
    {
        if (powerOn)
        {

        }
    }

    public void HandleNextToolPressed()
    {
        if (powerOn)
        {

        }
    }

    public void HandleToolReleasedPressed()
    {
        if (powerOn)
        {

        }
    }

    public void HandlePointZeroSetPressed()
    {
        if (powerOn)
        {

        }
    }

    //Display Region Methods

    public void HandleProgramConversionPressed()
    {

    }

    public void HandlePositionPressed()
    {

    }
    public void HandleOffsetPressed()
    {
        if(powerOn)
        {
            displayManager.HandleOffsetWindowState(true);
            
        }
    }
    public void HandleCurrentCommandsPressed()
    {
        if(powerOn)
        {
            displayManager.HandleCurrentCommandsWindowState(true);
        }
    }
    public void HandleAlarmMessagesPressed()
    {

    }
    public void HandleParameterDiagnosisPressed()
    { 

    }
    public void HandleSettingsGraphPressed()
    {

    }
    public void HandleHelpCalculationPressed()
    {

    }

    //Cursor region handlers

    public void HandleHomePressed()
    {
        if(powerOn)
        {
            messageWindow = true;
            displayManager.HandleHomeScreen();
        }
    }
    public void HandleUpPressed()
    {
        if (powerOn)
        {
            displayManager.HandleUpCursor();
        }
    }
    public void HandlePageUpPressed()
    {
        if (powerOn)
        {
          
        }
    }
    public void HandleLeftPressed()
    {
        if (powerOn)
        {
            displayManager.HandleLeftCursor();
        }
    }
    public void HandleRightPressed()
    {
        if (powerOn)
        {
            displayManager.HandleRightCursor();
        }
    }
    public void HandleEndPressed()
    {
        if (powerOn)
        {

        }
    }
    public void HandleDownPressed()
    {
        if (powerOn)
        {
              displayManager.HandleDownCursor();
        }
    }
    public void HandlePageDownPressed()
    {
        if (powerOn)
        {

        }
    }

    //EDIT Region Handlers
    private void HandleEditPressed()
    {
    }

    private void HandleInsertPressed()
    {
    }

    private void HandleAlterPressed()
    {
    }

    private void HandleDeletePressed()
    {
    }

    private void HandleUndoPressed()
    {
    }

    private void HandleMemPressed()
    {
    }

    private void HandleSingleBlockPressed()
    {

    }

    private void HandleDryRunPressed()
    {

    }

    private void HandleOptStopPressed()
    {

    }

    private void HandleBlockDeletePressed()
    {

    }

    private void HandleMdiDncPressed()
    {

    }

    private void HandleCoolntPressed()
    {

    }

    private void HandleSpindleJogPressed()
    {

    }

    private void HandleTurretFwdPressed()
    {

    }

    private void HandleTurretRevPressed()
    {

    }

    private void HandleHandleJogPressed()
    {

    }

    private void HandlePoint0001Pressed()
    {

    }

    private void HandlePoint001Pressed()
    {

    }

    private void HandlePoint01Pressed()
    {

    }

    private void HandlePoint1Pressed()
    {

    }

    private void HandleZeroRetPressed()
    {

    }

    private void HandleAllPressed()
    {

    }

    private void HandleOriginPressed()
    {

    }

    private void HandleSinglPressed()
    {

    }

    private void HandleHomeG28Pressed()
    {

    }

    private void HandleListProgPressed()
    {
        if (powerOn)
        {
            if(UsbConnected == true)
            {
                displayManager.HandleListProgScreen(true);
            }
            if(UsbConnected == false)
            {
                displayManager.HandleListProgWithoutUSB();            }
        }
    }

    private void HandleSelectProgPressed()
    {
        if(powerOn)
        {
            displayManager.HandleSelectProgramPressed();
            CodeReady = true;
        }
    }

    private void HandleSendPressed()
    {
 
    }

    private void HandleRecvPressed()
    {

    }

    private void HandleEraseProgPressed()
    {

    }



    //Numeric Region Handlers
    private void HandleSevenPressed()
    {

    }

    private void HandleEightPressed()
    {

    }

    private void HandleNinePressed()
    {

    }

    private void HandleFourPressed()
    {

    }

    private void HandleFivePressed()
    {

    }

    private void HandleSixPressed()
    {

    }

    private void HandleOnePressed()
    {

    }

    private void HandleTwoPressed()
    {

    }

    private void HandleThreePressed()
    {
 
    }

    private void HandleMinusPressed()
    {

    }

    private void HandleZeroPressed()
    {

    }

    private void HandleTimesPressed()
    {

    }

    private void HandleCancelPressed()
    {

    }

    private void HandleSpacePressed()
    {

    }

    private void HandleWritePressed()
    {
        if(powerOn)
        {
            
        }
    }







}
