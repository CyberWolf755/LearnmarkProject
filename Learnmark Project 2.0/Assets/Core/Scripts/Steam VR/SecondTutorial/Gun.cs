using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeldObject))]
public class Gun : MonoBehaviour {


    public GameObject Projectile;
    public float power;
    public Transform firepoint;

    public Valve.VR.EVRButtonId shootButton;

    public bool Automatic;
    public float CoolDownTime;
    float time;

    HeldObject heldObject;

    public MagSlot MagSlot;


	// Use this for initialization
	void Start ()
    {
        heldObject = GetComponent<HeldObject>();   
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(CoolDownTime > 0)
        {
            time -= Time.deltaTime;
        }

       /* if (MagSlot.currentMag != null)
        {*/

           // Magazine mag = MagSlot.currentMag;

            if (/*mag != null && mag.RoundsLeft > 0 && */heldObject.parent != null && ((heldObject.parent.controller.GetPressDown(shootButton) && !Automatic) || (heldObject.parent.controller.GetPress(shootButton) && Automatic)))
            {

                time = CoolDownTime;
                GameObject proj = (GameObject)Instantiate(Projectile, firepoint.position, firepoint.rotation);
                proj.GetComponent<Rigidbody>().velocity = firepoint.forward * power;
                //mag.RoundsLeft--;
            }
      //  }

        
    }
}
