using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRegionHandler : MonoBehaviour {

    // Callback signature
    public delegate void ProgramConversion();
    // event declaration
    public static event ProgramConversion WhenProgramConversionPressed;

    // Callback signature
    public delegate void Position();
    // event declaration
    public static event Position WhenPositionPressed;

    // Callback signature
    public delegate void Offset();
    // event declaration
    public static event Offset WhenOffsetPressed;

    // Callback signature
    public delegate void CurrentCommands();
    // event declaration
    public static event CurrentCommands WhenCurrentCommandsPressed;

    // Callback signature
    public delegate void AlarmMessages();
    // event declaration
    public static event AlarmMessages WhenAlarmMessagesPressed;

    // Callback signature
    public delegate void ParamaterDiagnosis();
    // event declaration
    public static event ParamaterDiagnosis WhenParameterDiagnosisPressed;

    // Callback signature
    public delegate void SettingsGraph();
    // event declaration
    public static event SettingsGraph WhenSettingsGraphPressed;

    // Callback signature
    public delegate void HelpCalculation();
    // event declaration
    public static event HelpCalculation WhenHelpCalculationPressed;


    public void HandleButtonByName(string ButtonName)
    {
      
            Debug.Log("Handling Button By name " + ButtonName);
            switch (ButtonName)
            {
                case "PrgrmConvrs":
                    HandlePrgrmConvrs();
                    break;
                case "Posit":
                    HandlePosit();
                    break;
                case "Offset":
                    HandleOffset();
                    break;
                case "CurntComds":
                    HandleCurntComds();
                    break;
                case "AlarmMesgs":
                    HandleAlarmMesgs();
                    break;
                case "ParamDgnos":
                    HandleParamDgnos();
                    break;
                case "SetngGraph":
                    HandleSetngGraph();
                    break;
                case "HelpCalc":
                    HandleHelpCalc();
                    break;
                default:
                        Debug.Log(ButtonName + " value not found");
                        break;
            }
    }

    public void HandlePrgrmConvrs()
    {
        if (WhenProgramConversionPressed != null)
        {
            WhenProgramConversionPressed();
        }
    }
    public void HandlePosit()
    {
        if(WhenPositionPressed != null)
        {
            WhenPositionPressed();
        }
    }
    public void HandleOffset()
    {
        if(WhenOffsetPressed != null)
        {
            WhenOffsetPressed();
        }
    }
    public void HandleCurntComds()
    {
        if(WhenCurrentCommandsPressed != null)
        {
            WhenCurrentCommandsPressed();
        }
    }
    public void HandleAlarmMesgs()
    {
        if(WhenAlarmMessagesPressed != null)
        {
            WhenAlarmMessagesPressed();
        }
    }
    public void HandleParamDgnos()
    {
        if(WhenParameterDiagnosisPressed != null)
        {
            WhenParameterDiagnosisPressed();
        }
    }
    public void HandleSetngGraph()
    {
        if(WhenSettingsGraphPressed != null)
        {
            WhenSettingsGraphPressed();
        }
    }
    public void HandleHelpCalc()
    {
        if(WhenHelpCalculationPressed != null)
        {
            WhenHelpCalculationPressed();
        }
    }
}


