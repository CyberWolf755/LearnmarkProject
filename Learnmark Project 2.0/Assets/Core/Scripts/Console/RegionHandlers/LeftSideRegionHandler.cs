using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideRegionHandler : MonoBehaviour {


    //TODO - Change theese to Actions

    // Callback signature
    public delegate void PoweredOn();
    // event declaration
    public static event PoweredOn WhenPoweredOn;

    // Callback signature
    public delegate void PoweredOff();
    // event declaration
    public static event PoweredOff WhenPoweredOff;

    // Callback signature
    public delegate void EmergencyStopped();
    // event declaration
    public static event EmergencyStopped WhenEmergencyStopped;

    // Callback signature
    public delegate void CycleStarted();
    // event declaration
    public static event CycleStarted WhenCycleStarted;

    // Callback signature
    public delegate void CycleStopped();
    // event declaration
    public static event CycleStopped WhenCycleStopped;



    public void HandleButtonByName(string ButtonName)
    {
        Debug.Log("Handling Button By name " + ButtonName);
        switch (ButtonName)
        {
            case "PowerOn":
                HandlePowerOn();
                break;
            case "PowerOff":
                HandlePowerOff();
                break;
            case "EmergencyStop":
                HandleEmergencyStop();
                break;
            case "CycleStart":
                HandleCycleStart();
                break;
            case "CycleStop":
                HandleCycleStop();
                break;
            default:
                Debug.Log(ButtonName + " value not found");
                break;
        }
    }   


    public void HandlePowerOn()
    {

        if (WhenPoweredOn != null)
        {
            WhenPoweredOn();
        }
        
        
    }

    public void HandlePowerOff()
    {

        if (WhenPoweredOff != null)
        {
            WhenPoweredOff();
        }
        
    }

    public void HandleEmergencyStop()
    {

        if (WhenEmergencyStopped != null)
        {
            WhenEmergencyStopped();
        }
    }

    public void HandleCycleStart()
    {

        if (WhenCycleStarted != null)
        {
            WhenCycleStarted();
        }
        
        
    }

    public void HandleCycleStop()
    {

        if (WhenCycleStopped != null)
        {
            WhenCycleStopped();
        }
        
    }






}
