
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConsoleKeyboardRegionEnum)), CanEditMultipleObjects]
public class ConsoleRegionEditor : Editor
{
    // private ConsoleKeyboardRegionEnum consoleReference;

    //  public ConsoleKeyboardRegionEnum.LeftSideButtonEnumerator LeftSideButtonName;

    public string SelectedValue;

     public SerializedProperty
      ConsoleRegionProperty,
      LeftSideControlsProperty,
      FunctionKeysProperty,
      JogKeysProperty,
      OverrideKeysProperty,
      DisplayKeysProperty,
      CursorKeysProperty,
      AlphaKeysProperty,
      ModeKeysProperty,
      NumericKeysProperty,
      SelectedRegionProperty,
      ButtonNameProperty;

    void OnEnable()
    {
        // Setup the SerializedProperties
         ConsoleRegionProperty = serializedObject.FindProperty("ConsoleKeyboardRegion");
        LeftSideControlsProperty = serializedObject.FindProperty("LeftSideButtonsRegion");
        FunctionKeysProperty = serializedObject.FindProperty("FunctionKeysRegion");
        JogKeysProperty = serializedObject.FindProperty("JogKeysRegion");
        OverrideKeysProperty = serializedObject.FindProperty("OverrideKeysRegion");
        DisplayKeysProperty = serializedObject.FindProperty("DisplayKeysRegion");
        CursorKeysProperty = serializedObject.FindProperty("CursorKeysRegion");
        AlphaKeysProperty = serializedObject.FindProperty("AlphaKeysRegion");
        ModeKeysProperty = serializedObject.FindProperty("ModeKeysRegion");
        NumericKeysProperty = serializedObject.FindProperty("NumericKeysRegion");
        SelectedRegionProperty = serializedObject.FindProperty("SelectedRegion");
        ButtonNameProperty = serializedObject.FindProperty("SelectedButton");

       // consoleReference = (ConsoleKeyboardRegionEnum)target;
    }

    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        // DrawDefaultInspector();

        serializedObject.Update();

        EditorGUILayout.PropertyField(ConsoleRegionProperty);

        ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator region = (ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator)ConsoleRegionProperty.enumValueIndex;

        SelectedRegionProperty.stringValue = region.ToString();
         
        switch (region)
        {
            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.LeftSide:
                //     EditorGUILayout.PropertyField(ConsoleKeyboardRegionEnum.LeftSideButtonEnumerator, new GUIContent("ButtonName"));
                //LeftSideButtonName = (ConsoleKeyboardRegionEnum.LeftSideButtonEnumerator)EditorGUILayout.EnumPopup(LeftSideButtonName);
               EditorGUILayout.PropertyField(LeftSideControlsProperty, new GUIContent("Left side controls"));
                ConsoleKeyboardRegionEnum.LeftSideButtonEnumerator leftSideButton = (ConsoleKeyboardRegionEnum.LeftSideButtonEnumerator)LeftSideControlsProperty.enumValueIndex;
                ButtonNameProperty.stringValue = leftSideButton.ToString();
                break;

            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.Function:
                EditorGUILayout.PropertyField(FunctionKeysProperty, new GUIContent("Function Keys"));
                ConsoleKeyboardRegionEnum.FunctionKeysEnumerator functionKeysButton = (ConsoleKeyboardRegionEnum.FunctionKeysEnumerator)FunctionKeysProperty.enumValueIndex;
                ButtonNameProperty.stringValue = functionKeysButton.ToString();
                break;

            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.Jog:
                EditorGUILayout.PropertyField(JogKeysProperty, new GUIContent("Jog Keys"));
                ConsoleKeyboardRegionEnum.JogKeysEnumerator jogKeysButton = (ConsoleKeyboardRegionEnum.JogKeysEnumerator)JogKeysProperty.enumValueIndex;
                ButtonNameProperty.stringValue =jogKeysButton.ToString();
                break;

            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.Override:
                EditorGUILayout.PropertyField(OverrideKeysProperty, new GUIContent("Override Keys"));
                ConsoleKeyboardRegionEnum.OverrideKeysEnumerator overrideKeysButton = (ConsoleKeyboardRegionEnum.OverrideKeysEnumerator)OverrideKeysProperty.enumValueIndex;
                ButtonNameProperty.stringValue = overrideKeysButton.ToString();
                break;

            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.Display:
                EditorGUILayout.PropertyField(DisplayKeysProperty, new GUIContent("Display Keys"));
                ConsoleKeyboardRegionEnum.DisplayKeysEnumerator displayKeysButton = (ConsoleKeyboardRegionEnum.DisplayKeysEnumerator)DisplayKeysProperty.enumValueIndex;
                ButtonNameProperty.stringValue = displayKeysButton.ToString();
                break;

            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.Cursor:
                EditorGUILayout.PropertyField(CursorKeysProperty, new GUIContent("Cursor Keys"));
                ConsoleKeyboardRegionEnum.CursorKeysEnumerator cursorKeysButton = (ConsoleKeyboardRegionEnum.CursorKeysEnumerator)CursorKeysProperty.enumValueIndex;
                ButtonNameProperty.stringValue = cursorKeysButton.ToString();
                break;

            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.Alpha:
                EditorGUILayout.PropertyField(AlphaKeysProperty, new GUIContent("Alpha Keys"));
                ConsoleKeyboardRegionEnum.AlphaKeysEnumerator alphaKeysButton = (ConsoleKeyboardRegionEnum.AlphaKeysEnumerator)AlphaKeysProperty.enumValueIndex;
                ButtonNameProperty.stringValue = alphaKeysButton.ToString();
                break;

            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.Mode:
                EditorGUILayout.PropertyField(ModeKeysProperty, new GUIContent("Mode Keys"));
                ConsoleKeyboardRegionEnum.ModeKeysEnumerator modeKeysButton = (ConsoleKeyboardRegionEnum.ModeKeysEnumerator)ModeKeysProperty.enumValueIndex;
                ButtonNameProperty.stringValue = modeKeysButton.ToString();
                break;
            case ConsoleKeyboardRegionEnum.ConsoleKeyboardRegionEnumerator.Numeric:
                EditorGUILayout.PropertyField(NumericKeysProperty, new GUIContent("Numeric Keys"));
                ConsoleKeyboardRegionEnum.NumericKeysEnumerator numericKeysButton = (ConsoleKeyboardRegionEnum.NumericKeysEnumerator)NumericKeysProperty.enumValueIndex;
                ButtonNameProperty.stringValue = numericKeysButton.ToString();
                break;

        }
        serializedObject.ApplyModifiedProperties();

    }
}
