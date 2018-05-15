using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRConnectableObject : VRInteractableObject2 {

	
public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;

    public GameObject attachPoint;

	[HideInInspector]
	public VRConnectionPoint slot;

	VRControllerInput2 controllerReference;

    [HideInInspector]
    public bool IsConnected;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == attachPoint && other.GetComponent<VRConnectionPoint>())
        {

            
            other.GetComponent<VRConnectionPoint>().AttachObject(this);
            //GetComponent<BoxCollider>().enabled = false;

            OverrideRelease();
            transform.position = other.transform.position;
        	transform.rotation = other.transform.rotation;
           // transform.parent = other.transform;
			transform.SetParent(other.transform);
		   

        }
    }


	public void OverridePickUp(VRControllerInput2 controller)
    {
        if(slot != null)
        {
            slot.DetachObject();
            Debug.Log("Detaching Mag");
        }

		

        Pickup(controller);
    }
    public void OverrideRelease()
    {
        if(slot == null)
        {
            Debug.Log("Slot is null... enabling box collider and dropping object");
           // GetComponent<BoxCollider>().enabled = true;
            Release(controllerReference);
        }
    }

	public override void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
	{
		Debug.Log("Button pressed");

		controllerReference = controller; 

		//If pickup button was pressed
		if (button == pickupButton)
		{
			OverridePickUp(controller);
			Debug.Log("Override Pickup Called");
		}
			
	}
	
	public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
	{
		//If pickup button was released
		if (button == pickupButton)
			Release(controller);
	}

}
