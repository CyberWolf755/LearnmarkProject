using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(Controller))]
public class Hand : MonoBehaviour
{
    GameObject heldObject;
    Controller controller;

    [HideInInspector]
    public Valve.VR.EVRButtonId pickUpButton;
    [HideInInspector]
    public Valve.VR.EVRButtonId dropButton;

    int prevCount = 0;

    // Use this for initialization
    void Start () {

        controller = GetComponent<Controller>();
        pickUpButton = Valve.VR.EVRButtonId.k_EButton_Grip;
        dropButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if(heldObject)
        {
           
            if((controller.controller.GetPressUp(pickUpButton) && heldObject.GetComponent<HeldObject>().dropOnRelease) || (controller.controller.GetPressDown(dropButton) && !heldObject.GetComponent<HeldObject>().dropOnRelease))
            {
                heldObject.GetComponent<HeldObject>().Drop();
                heldObject = null;

            }
        }
        else
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);
            int currCount = 0;
            foreach(Collider col in cols)
            {
                if(heldObject == null && col.GetComponent<HeldObject>() != null && col.GetComponent<HeldObject>().parent == null  && col.GetComponent<HeldObject>().CanPickUp)
                {
                    currCount++;
                }
            }
            if (currCount != prevCount) controller.controller.TriggerHapticPulse(3999);
                prevCount = currCount;

            if (controller.controller.GetPressDown(pickUpButton))
            {

                foreach(Collider col in cols)
                {
                    if(heldObject == null && col.GetComponent<HeldObject>() && col.GetComponent<HeldObject>().parent == null && col.GetComponent<HeldObject>().CanPickUp)
                    {
                        heldObject = col.gameObject;
                        heldObject.GetComponent<HeldObject>().parent = controller;
                        heldObject.GetComponent<HeldObject>().PickUp();

                    }
                }
            }
        }
	}
}
//If you wanna implement the touchPad
/* switch(controller.CurrentTouchPosition())
 {
     case TouchPosition.Up:
         print("up");
         break;
     case TouchPosition.Down:
         print("Down");
         break;
     case TouchPosition.Left:
         print("Left");
         break;
     case TouchPosition.Right:
         print("Right");
         break;
     default:
         print("off");
         break;
 }*/
 //asas