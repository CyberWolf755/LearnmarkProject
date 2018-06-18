using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerAnimationManager : MonoBehaviour {


    public bool canAnimate;
	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        if (canAnimate == true)
        {
            gameObject.GetComponent<Animator>().SetTrigger("StartRotation");
            Debug.Log("Rotating Spinner");
            canAnimate = false;
            
        }
    }

    public void StartAnimation()
    {
        gameObject.GetComponent<Animator>().SetTrigger("StartRotation");
        Debug.Log("Rotating Spinner");
    }
}
