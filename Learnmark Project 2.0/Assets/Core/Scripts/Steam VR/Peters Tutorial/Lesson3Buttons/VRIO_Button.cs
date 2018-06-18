using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRIO_Button : VRInteractableObject2 {

	//Which controller button will be used to click the button
	public EVRButtonId buttonToTrigger = EVRButtonId.k_EButton_SteamVR_Trigger;
	
	
	public delegate void ButtonPress();
	
	// On Button press event that will be called
	public static event ButtonPress OnButtonPress;

	//Position of the Button
	public Transform Button;

	//Where the button will travel to
	public Transform PointB;

	//Click speed
	public float buttonClickSpeed;

	//Transform of where the button will travel to
	//private Vector3 buttonDownPos;

	private Vector3 currentButtonDestination;
	private Vector3 buttonStartPos;
	public void Awake()
{
	VRIO_Button.OnButtonPress += MethodToTrigger;
	currentButtonDestination = Button.position;
	buttonStartPos = Button.position;
	//buttonDownPos = PointB.position;
}
 
protected void MethodToTrigger()
{
	//This method will be called any time the button is pressed,
	//and the event is Invoked. Note that since the event is static,
	//this method will fire any time ANY button of that type is pressed.
	Debug.Log("Method triggered");
	
}
	

	public void Update()
	{
		//Check to see if button is in the same position as its destination position
		if (Button.position != currentButtonDestination)
		{
			//If its not, lerp toward it at a predefined speed.
			//Remember to multiply movements in Update by Time.deltaTime, so that things don't move faster 
			//on computers with higher framerates
			Vector3 position = Vector3.MoveTowards(Button.position, currentButtonDestination, buttonClickSpeed * Time.deltaTime);
			Button.position = position;
		}
	}

	public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
	{
		//If button released is desired "trigger" button
		if (button == buttonToTrigger)
		{
			currentButtonDestination = buttonStartPos;
		}
	}
	
		public override void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
		{
			//If button is desired "trigger" button
			if (button == buttonToTrigger)
			{
			//Set button's destination position to the "down" position
			currentButtonDestination = PointB.position; //buttonDownPos;
	
			//TriggerButtonPress();
			OnButtonPress();
			}

		
		}

}