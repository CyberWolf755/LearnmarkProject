using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRIO_Parented : VRInteractableObject2
{
	public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;


	public override void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
	{
		//If pickup button was pressed
		if (button == pickupButton)
			Pickup(controller);
	}
	
	public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
	{
		//If pickup button was released
		if (button == pickupButton)
			Release(controller);
	}

}
