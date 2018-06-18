using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour {

    public GameObject KeyConenctionPointGameObj;
    private ToolPieceConnectionPoint KeyConnectionPoint;

    public GameObject ConnectedKeyTool;
    public GameObject DisconnectedKeyTool;

    [HideInInspector]
    public ConnectableTool connectableToolScript;


    public delegate void ScrewTightened();
    public event ScrewTightened OnScrewTightened;


    public delegate void ScrewUntightened();
    public event ScrewUntightened OnScrewUntightened;

    private bool screwTightened;


    // Use this for initialization
    void Start () {
        KeyConnectionPoint = KeyConenctionPointGameObj.GetComponent<ToolPieceConnectionPoint>();
        connectableToolScript = gameObject.GetComponent<ConnectableTool>();
    }
	

    public void SetKeyConnectionPointActive()
    {
        KeyConenctionPointGameObj.SetActive(true);
        KeyConnectionPoint.OnObjectConnected += HandleKeyToolConnected;
        //ConnectedKeyTool.GetComponent<RotatableKeyTool>().CheckScrewTightenedState();
        
    }

    public void SetKeyConnectionPointInactive()
    {
        KeyConenctionPointGameObj.SetActive(false);
        KeyConnectionPoint.OnObjectConnected -= HandleKeyToolConnected;
    }

    private void HandleKeyToolConnected(ConnectableTool connectedTool)
    {
     //   DisconnectedKeyTool = connectedTool.GetComponent<GameObject>();
        DisconnectedKeyTool.gameObject.SetActive(false);

        ConnectedKeyTool.SetActive(true);

        gameObject.GetComponent<VRInteractableObject2>().IsInteractable = false;
        /*gameObject.GetComponent<MeshCollider>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;*/

        
    }

    public void HandleKeyToolDisconnected()
    {
        ConnectedKeyTool.SetActive(false);
        DisconnectedKeyTool.SetActive(true);
        ConnectableTool toolScript = DisconnectedKeyTool.GetComponent<ConnectableTool>();
        VRControllerInput2 controllerRef = toolScript.controllerReference;
        KeyConenctionPointGameObj.SetActive(false);
        toolScript.OverridePickUp(controllerRef);
        DisconnectedKeyTool.gameObject.transform.position = controllerRef.gameObject.transform.position;
       

        StartCoroutine(WaitAndActivate());


        /*Rigidbody rb = DisconnectedKeyTool.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;*/


        
        /* gameObject.GetComponent<MeshCollider>().enabled = true;
         gameObject.GetComponent<CapsuleCollider>().enabled = true;*/

    }

    IEnumerator WaitAndActivate()
    {
       
        yield return new WaitForSeconds(2);

        KeyConenctionPointGameObj.SetActive(true);
        if(screwTightened == false) gameObject.GetComponent<VRInteractableObject2>().IsInteractable = true;


    }

    public void SendScrewTightened()
    {
        if (OnScrewTightened != null) OnScrewTightened();
        connectableToolScript.IsInteractable = false;
        connectableToolScript.isHighlightable = false;
        screwTightened = true;
    }
    public void SendScrewUntightened()
    {
        if (OnScrewUntightened != null) OnScrewUntightened();
        connectableToolScript.IsInteractable = true;
        connectableToolScript.isHighlightable = true;
        screwTightened = false;
    }
}
