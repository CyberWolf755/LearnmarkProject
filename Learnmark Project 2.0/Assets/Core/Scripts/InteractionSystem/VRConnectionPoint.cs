using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRConnectionPoint : MonoBehaviour {

	public VRConnectableObject currentObject;

   /* public Transform PointA;
    public Transform PointB;*/

	public void AttachObject(VRConnectableObject obj)
	{
		if(obj != null)
		{
			currentObject = obj;
			currentObject.slot = this;
            currentObject.IsConnected = true;
		}
	}

    public void DetachObject()
    {
        if(currentObject != null)
        {
            currentObject.slot = null;
            currentObject = null;
            currentObject.IsConnected = false;
        }
    }
}
