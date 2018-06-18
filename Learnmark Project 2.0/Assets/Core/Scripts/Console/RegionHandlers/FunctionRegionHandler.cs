using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionRegionHandler : MonoBehaviour {


    // Callback signature
    public delegate void ResetPressed();
    // event declaration
    public static event ResetPressed WhenResetPressed;

    // Callback signature
    public delegate void PoweredUp();
    // event declaration
    public static event PoweredUp WhenPowerUpPressed;

    // Callback signature
    public delegate void Recovered();
    // event declaration
    public static event Recovered WhenRecoverPressed;

    // Callback signature
    public delegate void F1();
    // event declaration
    public static event F1 WhenF1Pressed;

    // Callback signature
    public delegate void F2();
    // event declaration
    public static event F2 WhenF2Pressed;

    // Callback signature
    public delegate void F3();
    // event declaration
    public static event F3 WhenF3Pressed;

    // Callback signature
    public delegate void F4();
    // event declaration
    public static event F4 WhenF4Pressed;

    // Callback signature
    public delegate void ToolOffsetMeasure();
    // event declaration
    public static event ToolOffsetMeasure WhenToolOffsetMeasurePressed;

    // Callback signature
    public delegate void NextTool();
    // event declaration
    public static event NextTool WhenNextToolPressed;

    // Callback signature
    public delegate void ToolRelease();
    // event declaration
    public static event ToolRelease WhenToolReleasePressed;

    // Callback signature
    public delegate void PointZeroSet();
    // event declaration
    public static event PointZeroSet WhenPointZeroSetPressed;

    public void HandleButtonByName(string ButtonName)
    {
        Debug.Log("Handling Button By name " + ButtonName);
        switch (ButtonName)
        {
            case "Reset":
                HandleReset();
                break;
            case "PowerUp":
                HandlePowerUp();
                break;
            case "Recover":
                HandleRecover();
                break;
            case "F1":
                HandleF1();
                break;
            case "F2":
                HandleF2();
                break;
            case "F3":
                HandleF3();
                break;
            case "F4":
                HandleF4();
                break;
            case "ToolOffsetMeasure":
                HandleToolOffsetMeasure();
                break;
            case "NextTool":
                HandleNextTool();
                break;
            case "ToolRelease":
                HandleToolRelease();
                break;
            case "PointZeroSet":
                HandlePointZeroSet();
                break;
            default:
                Debug.Log(ButtonName + " value not found");
                break;
        }
    }

    public void HandleReset()
    {
        if(WhenResetPressed != null)
        {
            WhenResetPressed();
        }
    }

    public void HandlePowerUp()
    {
        if (WhenPowerUpPressed != null)
        {
            WhenPowerUpPressed();
        }
    }
    public void HandleRecover()
    {
        if ( WhenRecoverPressed != null)
        {
           WhenRecoverPressed();
        }
    }
    public void HandleF1()
    {
        if (WhenF1Pressed != null)
        {
            WhenF1Pressed();
        }
    }
    public void HandleF2()
    {
        if (WhenF2Pressed != null)
        {
            WhenF2Pressed();
        }

    }
    public void HandleF3()
    {
        if (WhenF3Pressed != null)
        {
            WhenF3Pressed();
        }
    }
    public void HandleF4()
    {
        if (WhenF4Pressed != null)
        {
            WhenF4Pressed();
        }
    }
    public void HandleToolOffsetMeasure()
    {
        if (WhenToolOffsetMeasurePressed != null)
        {
            WhenToolOffsetMeasurePressed();
        }
    }
    public void HandleNextTool()
    {
        if (WhenNextToolPressed != null)
        {
            WhenNextToolPressed();
        }
    }
    public void HandleToolRelease()
    {
        if (WhenToolReleasePressed != null)
        {
            WhenToolReleasePressed();
        }
    }
    public void HandlePointZeroSet()
    {
        if (WhenPointZeroSetPressed != null)
        {
            WhenPointZeroSetPressed();
        }
    }
}
