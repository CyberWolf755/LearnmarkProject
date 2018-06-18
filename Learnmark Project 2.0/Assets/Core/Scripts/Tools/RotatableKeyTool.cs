using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RotatableKeyTool : VRInteractableObject2
{


    public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    public EVRButtonId RotateButton = EVRButtonId.k_EButton_Grip;
    Transform parent;

    [SerializeField]
    private float rotationMultiplier = 50;
    [SerializeField]
    private float translationMultiplier = 0.001f;

    private Vector3 initialControllerPosition;
    private Vector3 controllerPositionChanged;
    private float deltax;
    int rotationDirection = 1;


    public GameObject AttachedScrew;
    private Vector3 attachedScrewStartingPosition;
    private Screw screwScript;

    private float maxTightenedValue = -0.04051686f;

    private float minTightenedValue = 0.00000001f ;

    private bool canBeScrewed = true;
    private bool canBeUnscrewed = false;

   // public float yTranslation;
    public float screwPositiony;

    private void Start()
    {
        attachedScrewStartingPosition = AttachedScrew.GetComponent<Transform>().localPosition;
        screwScript = AttachedScrew.GetComponent<Screw>();
    }

    private void Update()
    {

        CheckScrewTightenedState();

        if (parent != null)
        {
           

           controllerPositionChanged = parent.position;
            if (controllerPositionChanged.x > initialControllerPosition.x)
            {
                rotationDirection = 1;
            }
            if (controllerPositionChanged.x < initialControllerPosition.x)
            {
                rotationDirection = -1;
            }  

            if((canBeScrewed == true && rotationDirection == -1 || canBeUnscrewed == true && rotationDirection == 1) || canBeScrewed == true && canBeUnscrewed == true )
            {
                //This bad boy does the rotation
                deltax = Mathf.Abs(initialControllerPosition.x - controllerPositionChanged.x);

                // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + deltax * rotationMultiplier * rotationDirection, transform.localEulerAngles.z );
                //  transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + deltax * rotationMultiplier * rotationDirection, 90);
                AttachedScrew.transform.localEulerAngles = new Vector3(AttachedScrew.transform.localEulerAngles.x, AttachedScrew.transform.localEulerAngles.y + deltax * rotationMultiplier * rotationDirection * -1, AttachedScrew.transform.localEulerAngles.z);

                //yTranslation = AttachedScrew.transform.localPosition.y + deltax * translationMultiplier * rotationDirection * 100;
                AttachedScrew.transform.localPosition = new Vector3(AttachedScrew.transform.localPosition.x, AttachedScrew.transform.localPosition.y + deltax * translationMultiplier * rotationDirection, AttachedScrew.transform.localPosition.z);
            

            }



        }

    }

    public override void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
    {
        if (button == RotateButton)
        {
            parent = controller.GetComponent<Transform>();
            initialControllerPosition = parent.transform.position;

                    /*currentPostition = new Vector3(transform.position.x, 
                                                   transform.position.y,      //Used in the old mechanics. Will be kept for reference
                                                   transform.position.z);*/

        }
        if(button == pickupButton)
        {
            transform.parent.transform.parent.gameObject.GetComponent<Screw>().HandleKeyToolDisconnected();
        }
    }

    public override void ButtonPressUp(EVRButtonId button, VRControllerInput2 controller)
    {
        if (button == RotateButton)
        {
            parent = null;

        }

    }

    public void CheckScrewTightenedState()
    {
        screwPositiony = AttachedScrew.GetComponent<Transform>().localPosition.y;

        if (screwPositiony <= maxTightenedValue && canBeScrewed == true)
        {
            canBeScrewed = false;
            canBeUnscrewed = true;
            screwScript.SendScrewTightened();
        }

        if (screwPositiony >= minTightenedValue && canBeUnscrewed == true)
        {
            canBeScrewed = true;
            canBeUnscrewed = false;
        }

        if ((screwPositiony >= maxTightenedValue && screwPositiony <= minTightenedValue) && (canBeUnscrewed == false || canBeScrewed == false))
        {
            canBeScrewed = true;
            canBeUnscrewed = true;
            screwScript.SendScrewUntightened();
        }

    }

}
