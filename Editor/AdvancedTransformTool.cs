using UnityEditor;
using UnityEngine;

public class AdvancedTransformTool : EditorWindow
{
    private Vector3 deltaPosition = Vector3.zero;
    private Vector3 deltaScale = Vector3.one;
    private float uniformScale = 1.0f;
    private bool useUniformScale = true;
    
    [MenuItem(("Tools/AdvancedTransformTool"))]
    public static void ShowWindow()
    {
        GetWindow<AdvancedTransformTool>("Advanced Transform");
    }

    private void OnGUI()
    {
        GUILayout.Label("Transform", EditorStyles.boldLabel);
        
        deltaPosition = EditorGUILayout.Vector3Field("Delta Position", deltaPosition);
        if (GUILayout.Button("Apply Position"))
        {
            ApplyPositionDelta();
        }
        
        GUILayout.Label("Scale", EditorStyles.boldLabel);
        useUniformScale = EditorGUILayout.Toggle("Uniform Scale", useUniformScale);
        if (useUniformScale)
        {
            uniformScale = EditorGUILayout.FloatField("Scale Factor", uniformScale);
        }
        else
        {
            deltaScale = EditorGUILayout.Vector3Field("Scale Axis", deltaScale);
        }

        if (GUILayout.Button("Apply Scale"))
        {
            ApplyScale();
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

    private void ApplyScale()
    {
        if (Selection.transforms.Length == 0)
        {
            Debug.LogWarning("No objects selected");
            return;
        }
        Undo.RecordObjects(Selection.transforms, "Apply Scale");

        foreach (Transform t in Selection.transforms)
        {
            if (useUniformScale)
            {
                t.localScale *= uniformScale;
            }
            else
            {
                t.localScale = Vector3.Scale(t.localScale, deltaScale);
            }
        }
    }
}
