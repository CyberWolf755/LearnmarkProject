using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRDrawer : VRInteractableObject2
{

    public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    Transform parent;
    Transform realParent;
    public Transform pointA;
    public Transform pointB;

    Vector3 offset;

    private bool drawerMoving = false;






    private void Awake()
    {
        realParent = transform.parent;
    }
    void Update()
    {
        if (parent != null)
        {
            transform.position = ClosestPointOnLine(parent.position) - offset;
        }

        else
        {
            transform.position = ClosestPointOnLine(transform.position);
        }


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
        {
            PickupSliding(controller);
            drawerMoving = true;
        }
           

    }

    public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
    {
        //If pickup button was released
        if (button == pickupButton)
        {
            ReleaseSliding(controller);
            drawerMoving = false;
        }
           
    }


    public void PickupSliding(VRControllerInput2 controller)
    {


        parent = controller.GetComponent<Transform>();



    }

    //Make it slide after you let it go instead of just stopping in place
    public void ReleaseSliding(VRControllerInput2 controller)
    {


        controller.simulator.transform.position = transform.position + offset;

        /*parent = controller.simulator.transform;*/
        parent = realParent;

    }



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

        if (t <= 0)
        {
            return va;
        }
        if (t >= Vector3.Distance(va, vb))
        {
            return vb;
        }
        //Exact point in local position
        Vector3 vVector3 = vVector2 * t;

        //Exact world position of the closest point on the line
        Vector3 vClosestPoint = va + vVector3;

        return vClosestPoint;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "InteractableObject" && drawerMoving == true)
        {
            other.transform.parent = gameObject.transform;
        }
    }

}
