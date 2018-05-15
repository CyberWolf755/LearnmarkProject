using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour {

    private string buttonRegion;
    private string buttonName;

    public GameObject Computer;

    FunctionRegionHandler FunctionRegion;
    DisplayRegionHandler DisplayRegion;
    CursorRegionHandler CursorRegion;
    AlphaRegionHandler AlphaRegion;
    JogRegionHandler JogRegion;
    OverrideRegionHandler OverrideRegion;
    NumericRegionHandler NumericRegion;
    ModeRegionHandler ModeRegion;
    LeftSideRegionHandler LeftSideRegion;


    //This method will subscribe to evry button's click event and it is going to call an appropriate script depending on the button
    // Callback signature
    //public delegate void ButtonPress();

    // Use this for initialization
    /*void Awake () {
        OnButtonPress += MethodToTrigger;
    }*/
	
	// Update is called once per frame

    
    private	 void OnEnable()
	{

        FunctionRegion = Computer.GetComponent<FunctionRegionHandler>();
        DisplayRegion = Computer.GetComponent<DisplayRegionHandler>();
        CursorRegion = Computer.GetComponent<CursorRegionHandler>();
        AlphaRegion = Computer.GetComponent<AlphaRegionHandler>();
        JogRegion = Computer.GetComponent<JogRegionHandler>();
        OverrideRegion = Computer.GetComponent<OverrideRegionHandler>();
        NumericRegion = Computer.GetComponent<NumericRegionHandler>();
        ModeRegion = Computer.GetComponent<ModeRegionHandler>();
        LeftSideRegion = Computer.GetComponent<LeftSideRegionHandler>();

        VR_Button.OnButtonPress += MethodToTrigger;


        
	}

	private void OnDisable()
	{
			VR_Button.OnButtonPress -= MethodToTrigger;
	}

    public void MethodToTrigger(object sender, ConsoleKeyboardRegionEnum ButtonRegion, string ButtonName)
    {
        //Debug.Log( "Region: " +  ButtonRegion.name + "Name : " + ButtonName);


        switch (ButtonRegion.name)
        {
            case "Function":
                FunctionRegion.HandleButtonByName(ButtonName);
                break;
            case "Jog":
                JogRegion.HandleButtonByName(ButtonName);
                break;
            case "Override":
                OverrideRegion.HandleButtonByName(ButtonName);
                break;
            case "Display":
                DisplayRegion.HandleButtonByName(ButtonName);
                break;
            case "Cursor":
                CursorRegion.HandleButtonByName(ButtonName);
                break;
            case "Alpha":
                AlphaRegion.HandleButtonByName(ButtonName);
                break;
            case "Mode":
                ModeRegion.HandleButtonByName(ButtonName);
                break;
            case "Numeric":
                NumericRegion.HandleButtonByName(ButtonName);
                break;
            case "Left Side Controls":
                LeftSideRegion.HandleButtonByName(ButtonName);
                break;
            default:
                Debug.Log(ButtonRegion.name + " value not found");
                break;
        }


    }
	void Update () {
		
	}


    
}
