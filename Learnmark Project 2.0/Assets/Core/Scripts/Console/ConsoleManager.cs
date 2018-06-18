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

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip buttonPressedSound;

    [SerializeField]
    StateMachine stateMachine;


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


            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip =buttonPressedSound;
        

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

    public void MethodToTrigger(object sender, string ButtonRegion, string ButtonName, bool producesSound)
    {
        //Debug.Log( "Region: " +  ButtonRegion.name + "Name : " + ButtonName);

        if (producesSound == true && stateMachine.powerOn == true)
        { audioSource.PlayOneShot(buttonPressedSound); }


        switch (ButtonRegion)
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
            case "LeftSide":
                LeftSideRegion.HandleButtonByName(ButtonName);
                break;
            default:
                Debug.Log(ButtonRegion + " value not found");
                break;
        }


    }


    
}
