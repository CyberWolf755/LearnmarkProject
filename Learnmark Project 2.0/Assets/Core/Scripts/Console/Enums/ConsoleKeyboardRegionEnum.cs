using UnityEngine;

public class ConsoleKeyboardRegionEnum : MonoBehaviour
{
    [SerializeField]
    public enum ConsoleKeyboardRegionEnumerator
    {
        Function,
        Jog,
        Override,
        Display,
        Cursor,
        Alpha,
        Mode,
        Numeric,
        LeftSide
    }



    [SerializeField]
    public enum LeftSideButtonEnumerator
    {
        PowerOn, PowerOff,
        EmergencyStop,
        CycleStart,CycleStop
    }

    [SerializeField]
    public enum FunctionKeysEnumerator
    {
        Reset, PowerUp, Recover,
        F1, F2, F3, F4,
        ToolOffsetMeasure, NextTool, ToolRelease, PointZeroSet
    }


   
    [SerializeField]
    public enum JogKeysEnumerator
    {
        ChipFwd,BPlus,ZPlus,Yminus,ClntUp,
        ChipStop,XPlus,JogLock,XMinus,ClntDown,
        ChipRev,Yplus,ZMinus,AMinus,AuxClnt
    }

    [SerializeField]
    public enum OverrideKeysEnumerator
    {
        FeedRateMinus10,FeedRate100Percent,FeedRatePlus10,HandCntrlFeed,
        SpindleMinus10,Spindle100Percent,SpindlePlus10,HandCntrlSpin,
        CW,Stop,CCW,Spindle,
        Rapid5Percent,Rapid25Percent,Rapid50Percent,Rapid100Percent
}

    [SerializeField]
    public enum DisplayKeysEnumerator
    {
       PrgrmConvrs,Posit,Offset,CurntComds,
       AlarmMesgs,ParamDgnos,SetngGraph,HelpCalc
    }

    [SerializeField]
    public enum CursorKeysEnumerator
    {
       Home,Up,PageUp,
       Left,Right,
       End,Down,PageDown
    }

    [SerializeField]
    public enum AlphaKeysEnumerator
    {
       Shift,A,B,C,D,E,
       F,G,H,I,J,K,
       L,M,N,O,P,Q,
       R,S,T,U,V,W,
       X,Y,Z,EOB,OpenBracket,CloseBracket
    }

    [SerializeField]
    public enum ModeKeysEnumerator
    {
       Edit,Insert,Alter,Delete,Undo,
       Mem,SingleBlock,DryRun,OptionStop,BlockDelete,
       MdiDnc,Coolnt,OrientSpindle,AtcFwd,AtcRev,
       HandJog,Point0001,Point001,Point01,Point1,
       ZeroRet,All,Origin,Singl,HomeG28,
       ListProg,SelectProg,Send,Recv,EraseProg
    }

    [SerializeField]
    public enum NumericKeysEnumerator
    {
       Seven,Eight,Nine,
       Four,Five,Six,
       One,Two,Three,
       Minus,Zero,Times,
       Cancel,Space,Write
    }

    public ConsoleKeyboardRegionEnumerator ConsoleKeyboardRegion;
    public LeftSideButtonEnumerator LeftSideButtonsRegion;
    public FunctionKeysEnumerator FunctionKeysRegion;
    public JogKeysEnumerator JogKeysRegion;
    public OverrideKeysEnumerator OverrideKeysRegion;
    public DisplayKeysEnumerator DisplayKeysRegion;
    public CursorKeysEnumerator CursorKeysRegion;
    public AlphaKeysEnumerator AlphaKeysRegion;
    public ModeKeysEnumerator ModeKeysRegion;
    public NumericKeysEnumerator NumericKeysRegion;

    public string SelectedRegion;
    public string SelectedButton;
}
