using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR;

public class VRControllerInput2 : MonoBehaviour {

    public GameObject highlightedObject;
    List<Collider> ActiveTriggers;



    [HideInInspector]
    public Controller parent;

	[HideInInspector]
    public Rigidbody simulator;



	//Should only ever be one, but just in case one gets stuck
	protected Dictionary<EVRButtonId, List<VRInteractableObject2>> pressDownObjects;
	protected List<EVRButtonId> buttonsTracked;

	//Controller References
	protected SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device
	{
		get
		{
			return SteamVR_Controller.Input((int)trackedObj.index);
		}
	}
	
	public delegate void TouchpadPress();
	public static event TouchpadPress OnTouchpadPress;

	//VRInteractableObject2 closestInteractable;
	//GameObject closestInteractableObject;
	 Collider closestColidedObject;

	List<Collider> TriggerList = new List<Collider>();
	
	
	void Awake()
	{
		 simulator = new GameObject().AddComponent<Rigidbody>();
        simulator.name = "simulator";
        simulator.transform.parent = transform.parent;
        simulator.useGravity = false;




		trackedObj = GetComponent<SteamVR_TrackedObject>();
	
		//Instantiate lists
		pressDownObjects = new Dictionary<EVRButtonId, List<VRInteractableObject2>>();

		buttonsTracked = new List<EVRButtonId>()
		{
			EVRButtonId.k_EButton_SteamVR_Trigger,
			EVRButtonId.k_EButton_Grip
		};
	}

	void Update()
	{

		 if (parent != null)
        {
            simulator.velocity = (parent.transform.position - simulator.position) * 50f;
        }



		//Check through all desired buttons to see if any have been released
		EVRButtonId[] pressKeys = pressDownObjects.Keys.ToArray();
		for (int i = 0; i < pressKeys.Length; i++)
		{
			//If tracked button is released
			if (device.GetPressUp(pressKeys[i]))
			{
				//Get all tracked objects in that button's "pressed" list
				List<VRInteractableObject2> releaseObjects = pressDownObjects[pressKeys[i]];
				for (int j = 0; j < releaseObjects.Count; j++)
				{
					//Send button release through to interactable script
					releaseObjects[j].ButtonPressUp(pressKeys[i], this);
				}
	
				//Clear 
				pressDownObjects[pressKeys[i]].Clear();
			}
		}
	}


	void OnTriggerStay(Collider collider)
	{
		if(collider.tag.Equals("InteractableObject") && collider.gameObject.activeSelf)
		{
			collider = closestColidedObject;
			
			//If collider has a rigid body to report to
			if (collider.attachedRigidbody != null)
			{
				//If rigidbody's object has interactable item scripts, iterate through them
				VRInteractableObject2[] interactables = collider.gameObject.GetComponents<VRInteractableObject2>();
				for (int i = 0; i < interactables.Length; i++)
				{
					
					VRInteractableObject2 interactable = interactables[i];
					for (int b = 0; b < buttonsTracked.Count; b++)
					{
						//If a tracked button is pressed
						EVRButtonId button = buttonsTracked[b];
						if (device.GetPressDown(button))
						{

							


							//If we haven't already sent the button press message to this interactable
							//Safeguard against objects that have multiple colliders for one interactable script
							if (!pressDownObjects.ContainsKey(button) || !pressDownObjects[button].Contains(interactable))
							{
								//Send button press through to interactable script
								interactable.ButtonPressDown(button, this);

								
		
								//Add interactable script to a dictionary flagging it to recieve notice
								//when that same button is released
								if (!pressDownObjects.ContainsKey(button))
									pressDownObjects.Add(button, new List<VRInteractableObject2>());
		
								pressDownObjects[button].Add(interactable);
							}
						}
					}
				}
			}			
		}
	}



		//The list of colliders currently inside the trigger

	
	
	//called when something enters the trigger
	void OnTriggerEnter(Collider other)
	{
        
		//if the object is not already in the list
		if(other.tag.Equals("InteractableObject")  && !TriggerList.Contains(other) && other.gameObject.activeSelf && other.GetComponent<VRInteractableObject2>().IsInteractable == true)
		{
			if(TriggerList.Count > 0)
			{
			closestColidedObject.GetComponent<VRInteractableObject2>().UnHighlight();
			}
			//add the object to the list
			TriggerList.Add(other);
			CheckForClosest();

			if(TriggerList.Count > 0)
			{
			closestColidedObject.GetComponent<VRInteractableObject2>().Highlight();
                //highlightedObject = closestColidedObject.gameObject;
			}
			

		
		}
	}
	
	//called when something exits the trigger
	void OnTriggerExit(Collider other)
	{
        //if the object is in the list
		if(TriggerList.Contains(other))
		{
			closestColidedObject.GetComponent<VRInteractableObject2>().UnHighlight();
			//remove it from the list
			TriggerList.Remove(other);
			if(TriggerList.Count > 0)
			{
				CheckForClosest();
				closestColidedObject.GetComponent<VRInteractableObject2>().Highlight();
			}
		    
			if(TriggerList.Count <= 0)
            {
                closestColidedObject = null;
            }

			
		}
	}



	private void CheckForClosest()
	{

        float closestDistance = 10000f;
        ActiveTriggers = new List<Collider>();


        foreach (Collider other in TriggerList)
        {
            if(other.gameObject.activeSelf == true)
            {
                ActiveTriggers.Add(other);
            }
        }
        
         TriggerList = ActiveTriggers;


        foreach (Collider other in TriggerList) // for each object in the sphere...
		{
            if(other.gameObject.activeSelf != true)
            {
                TriggerList.Remove(other);               
            }
            else
            {
                float distance = Vector3.Distance(other.transform.position, transform.position);
                //find the closest object
                if (closestDistance > distance)
                {

                    closestDistance = distance;
                    //closestInteractable = other.gameObject.GetComponent<VRInteractableObject2>();
                    //closestInteractableObject = other.gameObject;
                    closestColidedObject = other;

                    //closest object. get component interactable object. call method
                }
            }
				

		}
	}
}
