using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToolPieceConnectionPoint : MonoBehaviour {

    
    private GameObject CurrentlyConnectedTool;
    public ConnectableTool CurrentTool;

    [SerializeField]
    public ToolTypeEnum[] toolTypes;


    //Events
    // Callback signature
    public delegate void ObjectConnected(ConnectableTool connectedTool);
    // event declaration
    public event ObjectConnected OnObjectConnected;

    // Callback signature
    public delegate void ObjectDisconnected(ConnectableTool connectedTool);
    // event declaration
    public event ObjectDisconnected OnObjectDisconnected;

  //  private BoxCollider objectCollision;




    private void Start()
    {
       // objectCollision = gameObject.GetComponent<BoxCollider>();
    }
    /* public Transform PointA;
     public Transform PointB;*/

    public void AttachObject(ConnectableTool tool)
    {
        if (tool != null && CurrentTool == null)
        {
            CurrentTool = tool;
            CurrentTool.slot = this;
            CurrentTool.IsConnected = true;

            CurrentTool.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            CurrentTool.gameObject.GetComponent<Rigidbody>().useGravity = false;

          //  objectCollision.enabled = false;

            if (OnObjectConnected != null) OnObjectConnected(CurrentTool);
        }
    }

    public void DetachObject()
    {
        if (CurrentTool != null)
        {
            if (OnObjectDisconnected != null) OnObjectDisconnected(CurrentTool);

            CurrentTool.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            CurrentTool.gameObject.GetComponent<Rigidbody>().useGravity = true;

            CurrentlyConnectedTool = null;

            CurrentTool.slot = null;
            CurrentTool.IsConnected = false;
            CurrentTool = null;

           // objectCollision.enabled = true;

            Debug.Log("Detaching Object");
        }

        else Debug.Log("No object to Detach!");
    }

    public void SwapAttachedObject(GameObject swappedObject)
    {

        if (swappedObject.GetComponent<ConnectableTool>() != null)
        {
            GameObject spawnedMaterial = Instantiate(swappedObject, transform.position, transform.rotation);

            CurrentlyConnectedTool = null;
            CurrentTool.slot = null;
            CurrentTool.IsConnected = false;
            CurrentTool.gameObject.SetActive(false);
            CurrentTool = null;


            ConnectableTool obj = spawnedMaterial.GetComponent<ConnectableTool>();
            AttachObject(obj);
            obj.ConnectObject(gameObject);


        }
        else
        {
            Debug.Log("Object is not connectable!");
        }
    }

    public void ForceDisconnectObject()
    {
        if (CurrentTool != null)
        {
            CurrentTool.DisconnectObject();
            DetachObject();
        }
    }


    /*public ToolTypeEnum ToolType
    {
        get
        {
            return toolType;
        }

        set
        {
            toolType = value;
        }
    }*/

}
