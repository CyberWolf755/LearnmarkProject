using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class VRSlidingObject : VRInteractableObject2 {


public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;
	 Transform parent;
    Transform realParent;
    public Transform pointA;
    public Transform pointB;

    Vector3 offset;

    //HeldObject heldObject;

    public UnityEvent HitA;

    public UnityEvent HitB;

    public UnityEvent ReleasedA;

    public UnityEvent ReleasedB;

    int state = 0;

    public int State
    {
        get { return state; }
    }
    int previousState = 0;


    private void Awake()
    {
        realParent = transform.parent;
    }
    void Update ()
	 {
		if(parent != null)
        {
            transform.position = ClosestPointOnLine(parent.position) - offset;
        }

        else
        {
            transform.position = ClosestPointOnLine(transform.position);
        }

        if (transform.position == pointA.position)
        {
            state = 1;
        }
        else if (transform.position == pointB.position)
        {
            state = 2;
        }
        else state = 0;

        if (state == 1 && previousState == 0)
            HitA.Invoke();
        else if (state == 2 && previousState == 0)
            HitB.Invoke();
        else if (state == 0 && previousState == 1)
            ReleasedA.Invoke();
        else if (state == 0 && previousState == 2)
            ReleasedB.Invoke();

        previousState = state;

        realParent = transform;
	}

    public void Print(string text)
    {
        Debug.Log(text);
    }




	public override void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
	{
		//If pickup button was pressed
		if (button == pickupButton)
			PickupSliding(controller);
	}
	
	public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
	{
		//If pickup button was released
		if (button == pickupButton)
			ReleaseSliding(controller);
	}


	 public void PickupSliding(VRControllerInput2 controller)
    {
        //parent = heldObject.parent.transform;

		parent = controller.GetComponent<Transform>();
        
       // offset = parent.position - transform.position;

		/*  parent = controller.parent.transform;

        offset = parent.position - transform.position;*/
    }

    //Make it slide after you let it go instead of just stopping in place
    public void ReleaseSliding(VRControllerInput2 controller)
    {
        //heldObject.simulator.transform.position = transform.position + offset;

        // parent = heldObject.simulator.transform;
        /* Release(controller);
          transform.SetParent(null);
       parent = transform;*/

         controller.simulator.transform.position = transform.position + offset;

        /*parent = controller.simulator.transform;*/
        parent = realParent;
         //parent = transform;
        // parent = null;
       // parent = realParent;
    }

   /* public void PickUp()
    {
        parent = heldObject.parent.transform;

        offset = parent.position - transform.position;
    }

    //Make it slide after you let it go instead of just stopping in place
    public void Drop()
    {
        heldObject.simulator.transform.position = transform.position + offset;

        parent = heldObject.simulator.transform;
    }*/

    Vector3 ClosestPointOnLine(Vector3 point)
    {
        Vector3 va = pointA.position + offset;
        Vector3 vb = pointB.position + offset;

        //offset from first point(va) to position of the controller
        Vector3 vVector1 = point - va;

        //Direction from vb to va
        Vector3 vVector2 = (vb - va).normalized;

        //get the float distance
        float t = Vector3.Dot(vVector2, vVector1);

        if(t <= 0)
        {
            return va;
        }
        if(t >= Vector3.Distance(va,vb))
        {
            return vb;
        }
        //Exact point in local position
        Vector3 vVector3 = vVector2 * t;

        //Exact world position of the closest point on the line
        Vector3 vClosestPoint = va + vVector3;

        return vClosestPoint;
    }
}
