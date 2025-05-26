using UnityEditor;
using UnityEngine;

public class AdvancedTransformTool : EditorWindow
{
    private Vector3 deltaPosition = Vector3.zero;
    
    [MenuItem(("Tools/AdvancedTransformTool"))]
    public static void ShowWindow()
    {
        GetWindow<AdvancedTransformTool>("Advanced Transform");
    }

    private void OnGUI()
    {
        GUILayout.Label("Advanced Transform Tool", EditorStyles.boldLabel);
        
        deltaPosition = EditorGUILayout.Vector3Field("Delta Position", deltaPosition);
        if (GUILayout.Button("Apply Position"))
        {
            ApplyPositionDelta();
        }
    }

    private void ApplyPositionDelta()
    {
        if (Selection.transforms.Length == 0)
        {
            Debug.LogWarning("No objects selected");
            return;
        }
        
        Undo.RecordObjects(Selection.transforms, "Apply Position Delta");

        foreach (Transform t in Selection.transforms)
        {
            t.position += deltaPosition;
        }
    }
}
