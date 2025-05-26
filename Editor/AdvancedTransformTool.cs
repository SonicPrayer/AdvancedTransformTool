using UnityEditor;
using UnityEngine;

public class AdvancedTransformTool : EditorWindow
{
    [MenuItem(("Tools/AdvancedTransformTool"))]
    public static void ShowWindow()
    {
        GetWindow<AdvancedTransformTool>("Advanced Transform");
    }

    private void OnGUI()
    {
        GUILayout.Label("Advanced Transform Tool", EditorStyles.boldLabel);
    }
}
