using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VR_RotatableObject : VRInteractableObject2 {


    public EVRButtonId pickupButton = EVRButtonId.k_EButton_SteamVR_Trigger;

    private Transform initialAttachPoint;
    private Transform startingTransform;
    private Vector3 ab;

    Transform parent;
    private Vector3 objectPosition;
    private Vector3 controllerPosition;
    private float angleToTurn;

    Vector3 position3 = new Vector3();

    GameObject emptyObject;


    Quaternion rotation;

    Vector3 rotateForce;

    //  private Transform controllerPosition;



    public enum KnobDirection
    {
        x,
        y,
        z
    }

     [Tooltip("The axis on which the knob should rotate. All other axis will be frozen.")]
        public KnobDirection direction = KnobDirection.x;
        [Tooltip("The minimum value of the knob.")]
        public float min = 0f;
        [Tooltip("The maximum value of the knob.")]
        public float max = 100f;
        [Tooltip("The increments in which knob values can change.")]
        public float stepSize = 1f;

    private void Start()
    {
        startingTransform = transform;
        ab = transform.forward;

        //objectPosition = startingPosition;

        //instantiate empty game object and set it as a child of the rotatable object
        emptyObject = new GameObject();
        emptyObject =Instantiate(emptyObject, gameObject.transform.position, transform.rotation);
        emptyObject.transform.parent = transform;
    }

    private void Update()
    {
        if(parent != null)
        {
            CalculateRotation();

           // transform.Rotate(angleToTurn,-90, -90);

           // emptyObject.transform.LookAt(parent);

            //transform.rotation = emptyObject.transform.rotation;

            //  Quaternion originalRot = transform.rotation;
            // transform.rotation = originalRot * Quaternion.AngleAxis(emptyObject.transform.localRotation.y, transform.for);
            // transform.localRotation.eulerAngles.y =
            /* Vector3 v3T = transform.localEulerAngles;
             v3T.y = v3T.y + angleToTurn;
             transform.localEulerAngles = v3T;*/
            //  transform.localEulerAngles = angleToTurn;
            // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, emptyObject.transform.localEulerAngles.y, transform.localEulerAngles.z);
           // transform.localEulerAngles = new Vector3(emptyObject.transform.localEulerAngles.x, -90, -90);
            // transform.eulerAngles = new Vector3(emptyObject.transform.eulerAngles.x, startingTransform.eulerAngles.y, startingTransform.eulerAngles.z); // -> kinda works
          //   transform.rotation = Quaternion.AngleAxis(30, Vector3.forward);
            //   Quaternion emptyVector = emptyObject.transform.GetComponent<Quaternion>();
            // emptyVector = Quaternion.LookRotation(position3, transform.forward);
            //  transform.rotation.y = emptyObject.transform.rotation.y;
            //  transform.localEulerAngles = new Vector3(0.0f, emptyVector.y, 0.0f);

            
            




        }

    }

    public override void ButtonPressDown(EVRButtonId button, VRControllerInput2 controller)
    {
        if (button == pickupButton)
        {
            parent = controller.GetComponent<Transform>();
            //startingPosition = transform.right;
            initialAttachPoint = gameObject.transform;
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
         position3 = (parent.position - initialAttachPoint.position);

      //  rotation = Quaternion.LookRotation(position3,transform.up);

       // transform.localRotation = rotation;
         angleToTurn = Vector3.Angle(transform.forward,position3);
       // transform.rotation = Quaternion.AngleAxis(angleToTurn, Vector3.up);*/

        gameObject.GetComponent<Rigidbody>().AddForceAtPosition(position3, initialAttachPoint.position, ForceMode.VelocityChange);

    }
}
