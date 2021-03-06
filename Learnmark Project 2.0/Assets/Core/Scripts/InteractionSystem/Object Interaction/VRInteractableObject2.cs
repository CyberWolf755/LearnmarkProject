﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRInteractableObject2 : MonoBehaviour {

    protected Rigidbody rigidBody;
    protected bool originalKinematicState;
    protected Transform originalParent;
    public bool IsInteractable = true;

    //Highlight object
    public bool isHighlightable = true;
    [SerializeField] Color highlightColor = new Color(1, 1, 0, 1);
    private Color initialColor;



    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        //get initial color from material
        if (isHighlightable)
        {
            initialColor = gameObject.GetComponent<MeshRenderer>().material.color;
        }
        

        //Capture object's original parent and kinematic state
        originalParent = transform.parent;
        originalKinematicState = rigidBody.isKinematic;
    }

    /// <summary>
    /// Called when button is pressed down while controller is inside object
    /// </summary>
    /// <param name="controller"></param>
    public virtual void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
    {
        //Empty. Overriden meothod only.

      
    }

    /// <summary>
    /// Called when button is released after an object has been "grabbed".
    /// </summary>
    /// <param name="controller"></param>

    public virtual void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
    {
	    //Empty. Overriden meothod only.

    }
    public virtual void Highlight()
    {
        //if object is highlightable when interacting with it. Change it´s color
        if (isHighlightable)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = highlightColor;
        }
    }

    public virtual void UnHighlight()
    {
        //reset objects material color to it´s initial one
        if (isHighlightable)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = initialColor;
        }
        
    }

    public void Pickup(VRControllerInput2 controller)
    {
        //Make object kinematic
        //(Not effected by physics, but still able to effect other objects with physics)
        rigidBody.isKinematic = true;

        //Parent object to hand
        transform.SetParent(controller.gameObject.transform);
        
    }

    
    public void Release(VRControllerInput2 controller)
    {
        //Make sure the hand is still the parent. 
        //Could have been transferred to anothr hand.
        if (transform.parent == controller.gameObject.transform)
        {
            //Return previous kinematic state
            rigidBody.isKinematic = originalKinematicState;

            //Set object's parent to its original parent
            if (originalParent != controller.gameObject.transform)
            {
                //Ensure original parent recorded wasn't somehow the controller (failsafe)
                transform.SetParent(originalParent);
            }
            else
            {
                transform.SetParent(null);
            }

            //Throw object
            rigidBody.velocity = controller.device.velocity;
            rigidBody.angularVelocity = controller.device.angularVelocity;
        }
    }


}
