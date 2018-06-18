using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericRegionHandler : MonoBehaviour {

    public delegate void Seven();
    public static event Seven WhenSevenPressed;

    public delegate void Eight();
    public static event Eight WhenEightPressed;

    public delegate void Nine();
    public static event Nine WhenNinePressed;

    public delegate void Four();
    public static event Four WhenFourPressed;

    public delegate void Five();
    public static event Five WhenFivePressed;

    public delegate void Six();
    public static event Six WhenSixPressed;

    public delegate void One();
    public static event One WhenOnePressed;

    public delegate void Two();
    public static event Two WhenTwoPressed;

    public delegate void Three();
    public static event Three WhenThreePressed;

    public delegate void Minus();
    public static event Minus WhenMinusPressed;

    public delegate void Zero();
    public static event Zero WhenZeroPressed;

    public delegate void Times();
    public static event Times WhenTimesPressed;

    public delegate void Cancel();
    public static event Cancel WhenCancelPressed;

    public delegate void Space();
    public static event Space WhenSpacePressed;

    public delegate void Write();
    public static event Write WhenWritePressed;


    public void HandleButtonByName(string ButtonName)
    {
        Debug.Log("Handling Button By name " + ButtonName);
        switch (ButtonName)
        {
            case "Seven":
                HandleSeven();
                break;
            case "Eight":
                HandleEight();
                break;
            case "Nine":
                HandleNine();
                break;
            case "Four":
                HandleFour();
                break;
            case "Five":
                HandleFive();
                break;
            case "Six":
                HandleSix();
                break;
            case "One":
                HandleOne();
                break;
            case "Two":
                HandleTwo();
                break;
            case "Three":
                HandleThree();
                break;
            case "Minus":
                HandleMinus();
                break;
            case "Zero":
                HandleZero();
                break;
            case "Times":
                HandleTimes();
                break;
            case "Cancel":
                HandleCancel();
                break;
            case "Space":
                HandleSpace();
                break;
            case "Write":
                HandleWrite();
                break;

            default:
                Debug.Log(ButtonName + " value not found");
                break;
        }
    }

    private void HandleSeven()
    {
        if (WhenSevenPressed != null) WhenSevenPressed();
    }

    private void HandleEight()
    {
        if (WhenEightPressed != null) WhenEightPressed();
    }

    private void HandleNine()
    {
        if (WhenNinePressed != null) WhenNinePressed();
    }

    private void HandleFour()
    {
        if (WhenFourPressed != null) WhenFourPressed();
    }

    private void HandleFive()
    {
        if (WhenFivePressed != null) WhenFivePressed();
    }

    private void HandleSix()
    {
        if (WhenSixPressed != null) WhenSixPressed();
    }

    private void HandleOne()
    {
        if (WhenNinePressed != null) WhenNinePressed();
    }

    private void HandleTwo()
    {
        if (WhenTwoPressed != null) WhenTwoPressed();
    }

    private void HandleThree()
    {
        if (WhenThreePressed != null) WhenThreePressed();
    }

    private void HandleMinus()
    {
        if (WhenMinusPressed != null) WhenMinusPressed();
    }

    private void HandleZero()
    {
        if (WhenZeroPressed != null) WhenZeroPressed();
    }

    private void HandleTimes()
    {
        if (WhenTimesPressed != null) WhenTimesPressed();
    }

    private void HandleCancel()
    {
        if (WhenCancelPressed != null) WhenCancelPressed();
    }

    private void HandleSpace()
    {
        if (WhenSpacePressed != null) WhenSpacePressed();
    }

    private void HandleWrite()
    {
        if (WhenWritePressed != null) WhenWritePressed();
    }
}
