using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRegionHandler : MonoBehaviour {


    // Callback signature
    public delegate void Home();
    // event declaration
    public static event Home WhenHomePressed;

    // Callback signature
    public delegate void Up();
    // event declaration
    public static event Up WhenUpPressed;

    // Callback signature
    public delegate void PageUp();
    // event declaration
    public static event PageUp WhenPageUpPressed;

    // Callback signature
    public delegate void Left();
    // event declaration
    public static event Left WhenLeftPressed;

    // Callback signature
    public delegate void Right();
    // event declaration
    public static event Right WhenRightPressed;

    // Callback signature
    public delegate void End();
    // event declaration
    public static event End WhenEndPressed;

    // Callback signature
    public delegate void Down();
    // event declaration
    public static event Down WhenDownPressed;

    // Callback signature
    public delegate void PageDown();
    // event declaration
    public static event PageDown WhenPageDownPressed;

    public void HandleButtonByName(string ButtonName)
    {
        Debug.Log("Handling Button By name " + ButtonName);
        switch (ButtonName)
        {
            case "Home":
                HandleHome();
                break;
            case "Up":
                HandleUp();
                break;
            case "PageUp":
                HandlePageUp();
                break;
            case "Left":
                HandleLeft();
                break;
            case "Right":
                HandleRight();
                break;
            case "End":
                HandleEnd();
                break;
            case "Down":
                HandleDown();
                break;
            case "PageDown":
                HandlePageDown();
                break;
            default:
                Debug.Log(ButtonName + " value not found");
                break;
        }
    }

    public void HandleHome()
    {
        if (WhenHomePressed != null)
            WhenHomePressed();
    }
    public void HandleUp()
    {
        if (WhenUpPressed != null) WhenUpPressed();
    }

    public void HandlePageUp()
    {
        if (WhenPageUpPressed != null) WhenPageUpPressed();
    }
    public void HandleLeft()
    {
        if (WhenLeftPressed != null) WhenLeftPressed();
    }
    public void HandleRight()
    {
        if (WhenRightPressed != null) WhenRightPressed();
    }
    public void HandleEnd()
    {
        if (WhenEndPressed != null) WhenEndPressed();
    }
    public void HandleDown()
    {
        if (WhenDownPressed != null) WhenDownPressed();
    }
    public void HandlePageDown()
    {
        if (WhenPageDownPressed != null) WhenPageDownPressed();
    }
}
