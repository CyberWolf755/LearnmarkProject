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

    public ConsoleKeyboardRegionEnumerator ConsoleKeyboardRegion;
}
