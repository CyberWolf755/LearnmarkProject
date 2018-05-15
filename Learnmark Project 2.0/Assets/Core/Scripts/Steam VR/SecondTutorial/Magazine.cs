using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {



	[HideInInspector]
	public MagSlot slot;
    MagSlot possibleSlot;

    private void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.GetComponent<MagSlot>())
        {

            
            other.GetComponent<MagSlot>().AttachMag(this);
            GetComponent<BoxCollider>().enabled = false;

            GetComponent<HeldObject>().Drop();
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            transform.parent = other.transform;

        }
    }


        public void PickUp()
    {
        if(slot != null)
        {
            slot.DetachMag();
            Debug.Log("Detaching Mag");
        }

        GetComponent<HeldObject>().DefaultPickup();
    }
    public void Drop()
    {
        if(slot == null)
        {
            Debug.Log("Slot is null... enabling box collider and dropping object");
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<HeldObject>().DefaultDrop();
        }
    }

    /*private void AttachMagSlot(MagSlot other)
    {
        other.AttachMag(this);

        GetComponent<BoxCollider>().enabled = false;

        GetComponent<HeldObject>().Drop();

        Transform attachPoint = transform.parent = slot.transform.Find("AttachPoint"); //AttachPoint??
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.parent = attachPoint;
    }*/


/* 
    Vector3 ClosestPointOnLine(Vector3 point, Vector3 pointA, Vector3 pointB)
    {
        Vector3 va = pointA;
        Vector3 vb = pointB;

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
*/
}
