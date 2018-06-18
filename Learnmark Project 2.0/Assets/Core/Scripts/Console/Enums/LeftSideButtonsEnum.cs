using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideButtonsEnum : MonoBehaviour {

    [SerializeField]
    public enum LeftSideButtonEnumerator
    {
        PowerOn,
        PowerOff,
        EmergencyStop,
        CycleStart,
        CycleStop
    }

    //public LeftSideButtonEnumerator ButtonId;
}
