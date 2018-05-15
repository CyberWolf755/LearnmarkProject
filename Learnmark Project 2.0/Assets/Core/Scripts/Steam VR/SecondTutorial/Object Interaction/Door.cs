using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeldObject))]
[RequireComponent(typeof(HingeJoint))]
public class Door : MonoBehaviour {

    public Transform Parent;

    public float MinRot;
    public float MaxRot;



	// Use this for initialization
	void Start () {
        JointLimits limits = new JointLimits();
        limits.min = MinRot;
        limits.max = MaxRot;
        GetComponent<HingeJoint>().limits = limits;
        GetComponent<HingeJoint>().useLimits = true;

    }
	
	// Update is called once per frame
	void Update () {
		
        if(Parent != null)
        {
            Vector3 targetDelta = Parent.position - transform.position;
            targetDelta.y = 0;
            //how far we are from where we wanna be facing
            float AngleDiff = Vector3.Angle(transform.forward, targetDelta);

            //figure out which direction we wanna be rotating in.
            //if you didnt have the cross it would just rotate in circles
            Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

            GetComponent<Rigidbody>().angularVelocity = cross * AngleDiff * 50f;

        }
	}

    public void PickUp()
    {
        Parent = GetComponent<HeldObject>().parent.transform;
    }

    public void Drop()
    {
        Parent = null;
    }
}
