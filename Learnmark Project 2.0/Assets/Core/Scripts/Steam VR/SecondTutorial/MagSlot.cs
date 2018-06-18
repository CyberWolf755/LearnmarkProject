using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagSlot : MonoBehaviour {

	public Magazine currentMag;

   /* public Transform PointA;
    public Transform PointB;*/

	public void AttachMag(Magazine mag)
	{
		if(currentMag == null)
		{
			currentMag = mag;
			currentMag.slot = this;
		}
	}

    public void DetachMag()
    {
        if(currentMag != null)
        {
            currentMag.slot = null;
            currentMag = null;
        }
    }
	
}
