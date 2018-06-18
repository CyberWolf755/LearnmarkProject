using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRConnectionPoint : MonoBehaviour {
    private GameObject CurrentlyConnectedObject;
	public VRConnectableObject currentObject;
    public UnityEvent ObjectConnected;
    public UnityEvent ObjectDisconnected;

    /* public Transform PointA;
     public Transform PointB;*/

    public void AttachObject(VRConnectableObject obj)
	{
		if(obj != null)
		{
            obj.gameObject.GetComponent<Rigidbody>().isKinematic = true;

           currentObject = obj;
			currentObject.slot = this;
            currentObject.IsConnected = true;
            ObjectConnected.Invoke();
        }
	}

    public void DetachObject()
    {
        if (currentObject != null)
        {
            currentObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject.gameObject.GetComponent<Rigidbody>().useGravity = true;

            CurrentlyConnectedObject = null;

            currentObject.slot = null;
            currentObject.IsConnected = false;
            currentObject = null;
            ObjectDisconnected.Invoke();
        }

        else Debug.Log("No object to Detach!");
    }

    public void SwapAttachedObject(GameObject swappedObject)
    {

        if (swappedObject.GetComponent<VRConnectableObject>() != null)
        {
            GameObject spawnedMaterial = Instantiate(swappedObject, transform.position, transform.rotation);
           
            CurrentlyConnectedObject = null;
            currentObject.slot = null;
            currentObject.IsConnected = false;
            currentObject.gameObject.SetActive(false);
            currentObject = null;


            VRConnectableObject obj = spawnedMaterial.GetComponent<VRConnectableObject>();
              AttachObject(obj);
              obj.ConnectObject(gameObject);


        }
        else
        {
            Debug.Log("Object is not connectable!");
        }
    }
}
