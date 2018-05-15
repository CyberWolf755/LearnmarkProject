using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WandController : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();

    private InteractableItem closestItem;
    private InteractableItem interactingItem;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(controller == null)
        {
            Debug.Log("Controller not initialized.");
            return;
        }

        if (controller.GetPressDown(gripButton))
        {
            float minDistance = float.MaxValue;

            float distance;
            foreach (InteractableItem item in objectsHoveringOver)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;


                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }

            interactingItem = closestItem;

            if(interactingItem)
            {
                //in case we are holding the object with one hand and trying to pass it to the other
                if(interactingItem.IsInteracting())
                {
                    interactingItem.EndInteraction(this);
                }

                interactingItem.BeginInteraction(this);
            }
        }
       
        if (controller.GetPressUp(gripButton) && interactingItem != null)
        {
            interactingItem.EndInteraction(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableItem collidedItem = other.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Add(collidedItem);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        InteractableItem collidedItem = other.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Remove(collidedItem);
        }
    }
}
