using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;

public class VR_Button : VRInteractableObject2
{
    //TODO- add 2 serializable fields, a string which has a string enum name value
    //of the button and an enum string that has a value of the regieon it is in

   [HideInInspector]
    public ConsoleKeyboardRegionEnum ButtonRegion;


    

    //[SerializeField]
    public string ButtonName;

    //Which controller button will be used to click the button
    public EVRButtonId buttonToTrigger = EVRButtonId.k_EButton_SteamVR_Trigger;

    // Callback signature
    public delegate void ButtonPress(object sender, ConsoleKeyboardRegionEnum ButtonRegion, string ButtonName);
	// event declaration
	public static event ButtonPress OnButtonPress;

    [SerializeField]
    private bool switchTypeBehaviour = false;
    private bool switchState = false;

	/*public event Action OnButtonPress;*/




	//Where the button will travel to
	public Transform PointB;
    //Offset will be used if pointB is not set
	[SerializeField]
	private float offset = 0.01f;

	//Click speed
    [SerializeField]
	private float buttonClickSpeed = 1;


	private Vector3 currentButtonDestination;
	private Vector3 buttonStartPos;
	private Vector3 endPosition;
	public void Awake()
    {
        if(transform.parent.transform.parent.gameObject.GetComponent<ConsoleKeyboardRegionEnum>() != null)
        ButtonRegion = transform.parent.transform.parent.gameObject.GetComponent<ConsoleKeyboardRegionEnum>();
        if(transform.parent.gameObject.GetComponent<ConsoleKeyboardRegionEnum>() != null && ButtonRegion == null)
            ButtonRegion = transform.parent.gameObject.GetComponent<ConsoleKeyboardRegionEnum>();
        if(ButtonRegion == null)
        {
            if (transform.parent.transform.parent.transform.parent.gameObject.GetComponent<ConsoleKeyboardRegionEnum>() != null)
                ButtonRegion = transform.parent.transform.parent.transform.parent.gameObject.GetComponent<ConsoleKeyboardRegionEnum>();
        }
   


        currentButtonDestination = gameObject.transform.position;
	    buttonStartPos = gameObject.transform.position;


	    if(PointB != null)
	    {
		    endPosition = PointB.position;
	    }

	    if(PointB == null)
	    {
		
		    float x = buttonStartPos.x;
		    float y = buttonStartPos.y;
		    float z = buttonStartPos.z + (offset * transform.position.z);
		    endPosition = new Vector3(x,y,z);
	    }

    }
 
	

	public void Update()
	{
		//Check to see if button is in the same position as its destination position
		if (gameObject.transform.position != currentButtonDestination)
		{
			//If its not, lerp toward it at a predefined speed.
			//Remember to multiply movements in Update by Time.deltaTime, so that things don't move faster 
			//on computers with higher framerates
			Vector3 position = Vector3.MoveTowards(gameObject.transform.position, currentButtonDestination, buttonClickSpeed * Time.deltaTime);
			gameObject.transform.position = position;
		}
	}

	public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
	{

        if (!switchTypeBehaviour)
        {
            //If button released is desired "trigger" button
            if (button == buttonToTrigger)
            {
                currentButtonDestination = buttonStartPos;
              
            }
        }
       

    }
	
		public override void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
		{

        if (!switchTypeBehaviour)
        {
            //If button is desired "trigger" button
            if (button == buttonToTrigger)
            {
                //Set button's destination position to the "down" position
                currentButtonDestination = endPosition; //buttonDownPos;

                //TriggerButtonPress();
                if (OnButtonPress != null) OnButtonPress(this, ButtonRegion, ButtonName);

               
            }
        }
        else if (switchTypeBehaviour)
        {
            //If button released is desired "trigger" button
            if (button == buttonToTrigger)
            {
               if(!switchState)
                {
                    //Set button's destination position to the "down" position
                    currentButtonDestination = endPosition; //buttonDownPos;
                    switchState = true;
                    //TriggerButtonPress();
                    if (OnButtonPress != null) OnButtonPress(this, ButtonRegion, ButtonName);
                    
                }
                else if (switchState)
                {
                    currentButtonDestination = buttonStartPos;
                    switchState = false;
                    if (OnButtonPress != null) OnButtonPress(this, ButtonRegion, ButtonName);
                }
            }
        }




    }

}
