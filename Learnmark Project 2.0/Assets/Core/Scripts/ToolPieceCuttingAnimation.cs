using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPieceCuttingAnimation : MonoBehaviour {

    public ToolManager CurrentToolSlot;

    public GameObject Spinner;
    public GameObject SlidingWall;
    public GameObject SparksParticles;

    [SerializeField]
    private Transform positionB;
    private Transform positionA;

    public bool CanStartAnimation; //TODO: Change to private


    public Animator SpinnerAnimator;
    public SpinnerAnimationManager spinnerAnimationReference;
    public Animator SlidingWallAnimator;
    private Animator mainAnimator;

    public GameObject MaterialConnectionPoint;
    public GameObject ChangedMaterial;

    public GameObject CreatedObject;
    public Transform CreatedObjectSpawnPoint;

    private bool materialConnected = false;
    private bool cuttingToolConnected = false;
    private bool toolHolderConnected = false;
    private bool cuttingToolTightened = false;
    private bool toolHolderTightened = false;



    // Use this for initialization
    void Start () {
        /*positionA = gameObject.GetComponent<Transform>();
        journeyLength = Vector3.Distance(positionA.position, positionB.position);*/
        mainAnimator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if(CanStartAnimation == true)
        {
            /* if(toolHolderTightened == true)
             {
                 mainAnimator.SetBool("NormalConditions", true);
                 mainAnimator.SetTrigger("StartCutting");

             }
             if(cuttingToolTightened == false)
             {
                 mainAnimator.SetBool("CuttingToolUntightened", true);
             }*/

            gameObject.GetComponent<Animator>().SetTrigger("StartCutting");
            
            CanStartAnimation = false;
        }
	}

    public void StartAnimation()
    {
       
        SlidingWallAnimator.SetTrigger("StartWallMovement");
        // SpinnerAnimator.SetTrigger("StartRotation");
        spinnerAnimationReference.StartAnimation();

        Debug.Log("Starting Cutting Animation");

        /*if (cuttingToolConnected && !cuttingToolTightened)
        {
            //TODO notify main animator to play corresponding animation
            //Cutting tool falling out animation
        }
        if (cuttingToolConnected && cuttingToolTightened)
        {
            //TODO notify main animator to play corresponding animation
            //normal cutting animation
        }
      */

    }
    public void StopAnimation()
    {
        mainAnimator.StopPlayback();
    }

    public void ChangeMaterial()
    {
        if(cuttingToolConnected == true && cuttingToolTightened == true)
        {
            Instantiate(CreatedObject, CreatedObjectSpawnPoint.position, CreatedObjectSpawnPoint.rotation);
            MaterialConnectionPoint.GetComponent<VRConnectionPoint>().SwapAttachedObject(ChangedMaterial);
        }
    }

    public void ActivateSparks()
    {
        if (materialConnected == true && cuttingToolTightened == true) SparksParticles.SetActive(true);
    
    }
    public void DeactivateSparks()
    {
       if(SparksParticles.activeSelf == true) SparksParticles.SetActive(false);
    }
   
    public void SetStartAnimation(ToolManager toolMngr, bool materialIsConnected)
    {
        cuttingToolConnected = toolMngr.CuttingToolConnected;
        cuttingToolConnected = toolMngr.ToolHolderConnected;
        cuttingToolTightened = toolMngr.CuttingToolTightened;
        toolHolderTightened = toolMngr.ToolHolderTightened;
        materialConnected = materialIsConnected;

        CurrentToolSlot = toolMngr;

        CanStartAnimation = true;
    }

    public void SendDetachCheckToToolManager()
    {
        CurrentToolSlot.ForceDisconnectUntightened();
    }
}
