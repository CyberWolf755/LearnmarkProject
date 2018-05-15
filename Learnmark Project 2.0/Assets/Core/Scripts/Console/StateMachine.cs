using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    public bool powerOn = false;
    public bool emergencyStop = false;
    public bool cycleStart = false;
    public bool doorClosed = true;

    //TODO - Move these into another script
    public float TimeToMakeObject;
    public GameObject CreatedObject;
    public Transform CreatedObjectSpawnPoint;
    [SerializeField] AudioSource audioSource;

    IEnumerator co;



    private void Awake()
    {
        LeftSideRegionHandler.WhenPoweredOn += HandlePoweredOn;
        LeftSideRegionHandler.WhenPoweredOff += HandlePoweredOff;
        LeftSideRegionHandler.WhenEmergencyStopped += HandleEmergencyStopped;
        LeftSideRegionHandler.WhenCycleStarted += HandleCycleStarted;
        LeftSideRegionHandler.WhenCycleStopped += HandleCycleStopped;

        co = StartMakingObject();

    }
    // Use this for initialization
   
    public void HandlePoweredOn()
    {
        if(!emergencyStop)
        {
            powerOn = true;
            Debug.Log("TurningOn");
        }
       
    }
    public void HandlePoweredOff()
    {
        if (!emergencyStop)
        {
            powerOn = false;
            cycleStart = false;
            Debug.Log("TurningOff");
        }
    }
    public void HandleEmergencyStopped()
    {
        emergencyStop = !emergencyStop;
        if(emergencyStop == true)
        {
            cycleStart = false;
            powerOn = false;
        }
        Debug.Log("Emergency stopped pressed/unpressed");

    }
    public void HandleCycleStarted()
    {
        if (!emergencyStop)
        {
            if (powerOn)
            {
                if (doorClosed)
                {
                    
                        cycleStart = true;
                        audioSource.Play();
                        Instantiate(CreatedObject, CreatedObjectSpawnPoint);
                        // StartCoroutine(co);
                        Debug.Log("Starting Cycle");
                    
                }
            }
        }
    }
    public void HandleCycleStopped()
    {
        if (!emergencyStop)
        {
            if (powerOn)
            {
                if(doorClosed)
                {
                    cycleStart = false;
                   // StopCoroutine(co);

                    Debug.Log("Stopping Cycle");
                }
            }
        }
    }

    public void HandleDoorOpened()
    {
        doorClosed = false;
        Debug.Log("Door Opened");
        cycleStart = false;
    }

    public void HandleDoorClosed()
    {
        doorClosed = true;
        Debug.Log("Door Closed");
        cycleStart = false;
    }

    private IEnumerator StartMakingObject()
    {
        while(cycleStart == true)
        {

            
            
            if(TimeToMakeObject == 0f)
            {
               
                
                yield return null;
            }
        }
       

    }

    

}
