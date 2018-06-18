using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ConnectableTool : VRInteractableObject2
{

    public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;


    //TODO remove and use enum
    //public GameObject[] attachPoints;

    //Slot is used to store the reference of the object the tool is connected to
    [HideInInspector]
    public ToolPieceConnectionPoint slot;

    [SerializeField]
    private ToolTypeEnum toolType;

    


    public VRControllerInput2 controllerReference;

    [HideInInspector]
    public bool IsConnected;

    private Transform initialParentTransform;

    private Vector3 initialScale;

    

    private void Start()
    {
        initialParentTransform = transform.parent;

        initialScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ToolPieceConnectionPoint>() && transform.parent == controllerReference.gameObject.transform)
        {
            ToolPieceConnectionPoint otherPoint = other.GetComponent<ToolPieceConnectionPoint>();

            //if other is a Tool Piece Connection point and accepts the same tool type
            foreach (ToolTypeEnum type in otherPoint.toolTypes)
            {
                if (type == toolType)
                {
                    //if it´s connected to a connection point Release it
                    OverrideRelease();

                    other.GetComponent<ToolPieceConnectionPoint>().AttachObject(this);
                    ConnectObject(other.gameObject);

                    break;
                }
            }
        }
       
       



        //TODO remove array of GameObjects and use an enum to check if object should be attached
        /*
        foreach (GameObject attachPoint in attachPoints)
        {
            if (other.gameObject == attachPoint && other.GetComponent<ToolPieceConnectionPoint>())
            {
                OverrideRelease();

                other.GetComponent<ToolPieceConnectionPoint>().AttachObject(this);
                ConnectObject(other.gameObject);


            }
        }
        */

    }

    //Funct
    public void ConnectObject(GameObject other)
    {

        transform.position = other.transform.position;
        transform.rotation = other.transform.rotation;
        transform.SetParent(other.transform);

        Debug.Log("Local position" + transform.localPosition);
        Debug.Log("World position" + transform.position);

    }

    public void DisconnectObject()
    {
        //transform.SetParent(null);

        transform.SetParent(initialParentTransform);
        transform.localScale = initialScale;

        Rigidbody objectRB = gameObject.GetComponent<Rigidbody>();
        objectRB.isKinematic = false;
        objectRB.useGravity = true;     //Might give errors with tool holder in the future
    }

    public void OverridePickUp(VRControllerInput2 controller)
    {
        if (slot != null)
        {
            slot.DetachObject();
        }
        Pickup(controller);
    }

    public void OverrideRelease()
    {
        if (slot == null)
        {
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
            Debug.Log("Correct button pressed");
        }
    }

    public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
    {
        //If pickup button was released
        if (button == pickupButton)
        {
            Release(controller);
        }
            
    }
}
