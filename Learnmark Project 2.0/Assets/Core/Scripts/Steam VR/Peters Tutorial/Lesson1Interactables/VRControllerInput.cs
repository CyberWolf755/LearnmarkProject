using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRControllerInput : MonoBehaviour {


    //Should only ever be one, but just in case
    protected List<VRInteractableObject> heldObjects;

    //Controller References
    protected SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Awake()
    {
        //Instantiate lists
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        heldObjects = new List<VRInteractableObject>();
    }

    void OnTriggerStay(Collider collider)
    {
        //If object is an interactable item
        VRInteractableObject interactable = collider.GetComponent<VRInteractableObject>();
        if (interactable != null)
        {
            //If trigger button is down
            if (device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                //Pick up object
                interactable.Pickup(this);
                heldObjects.Add(interactable);
            }
        }
    }

}
