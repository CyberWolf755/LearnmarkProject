using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VR_RotatableObject : VRInteractableObject2 {


    public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    Transform parent;

    [SerializeField]
    private float rotationMultiplier = 50;

    private Vector3 interactionStartedPosition;
    private Vector3 positionChanged;
    private float deltax;
    int rotationDirection = 1;




    private void Start()
    {

    }

    private void Update()
    {
        if(parent != null)
        {
            CalculateRotation();


            positionChanged = parent.position;
            if (positionChanged.x > interactionStartedPosition.x)
            {
                rotationDirection = 1;
            }
            if (positionChanged.x < interactionStartedPosition.x)
            {
                rotationDirection = -1;
            }

            
            //This is different rotation mechanics. also works but it is a little bit wonky
          /*  Vector3 targetPostition = new Vector3(parent.position.x,
                                          parent.position.y,
                                          currentPostition.z);

            float angle = Vector3.Angle(currentPostition, targetPostition) * rotationMultiplier * rotationDirection; //This WORKS!

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
            */


            //This bad boy does the rotation
             deltax = Mathf.Abs(interactionStartedPosition.x - positionChanged.x);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + deltax * rotationMultiplier * rotationDirection);


        }

    }

    public override void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
    {
        if (button == pickupButton)
        {
            parent = controller.GetComponent<Transform>();
            interactionStartedPosition = parent.transform.position;

            //         currentPostition = new Vector3(transform.position.x, 
            //                                        transform.position.y,      //Used in the old mechanics. Will be kept for reference
            //                                         transform.position.z);

        }
    }

    public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
    {
        if (button == pickupButton)
        {
            parent = null;

        }
      
    }

    private void CalculateRotation()
    {
      

    }
}
