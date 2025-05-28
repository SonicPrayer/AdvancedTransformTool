using UnityEditor;
using UnityEngine;

public class AdvancedTransformTool : EditorWindow
{
    private Vector3 _deltaPosition = Vector3.zero;
    private Vector3 _deltaScale = Vector3.one;
    private float _uniformScale = 1.0f;
    private bool _useUniformScale = true;
    
    [MenuItem(("Tools/AdvancedTransformTool"))]
    public static void ShowWindow()
    {
        GetWindow<AdvancedTransformTool>("Advanced Transform");
    }

    private void OnGUI()
    {
        GUILayout.Label("Transform", EditorStyles.boldLabel);
        _deltaPosition = EditorGUILayout.Vector3Field("Delta Position", _deltaPosition);
        if (GUILayout.Button("Apply Position"))
        {
            ApplyPositionDelta();
        }
        
        GUILayout.Label("Scale", EditorStyles.boldLabel);
        _useUniformScale = EditorGUILayout.Toggle("Uniform Scale", _useUniformScale);
        if (_useUniformScale)
        {
            _uniformScale = EditorGUILayout.FloatField("Scale Factor", _uniformScale);
        }
        else
        {
            _deltaScale = EditorGUILayout.Vector3Field("Scale Axis", _deltaScale);
        }

        if (GUILayout.Button("Apply Scale"))
        {
            ApplyScale();
        }
        
        GUILayout.Label("Alignment", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Align X"))
        {
            AlignSelectedObjects(0);
        }
        if (GUILayout.Button("Align Y"))
        {
            AlignSelectedObjects(1);
        }
        if (GUILayout.Button("Align Z"))
        {
            AlignSelectedObjects(2);
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Center Align"))
        {
            CenterAlingSelectedObjects();
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
            t.position += _deltaPosition;
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
            if (_useUniformScale)
            {
                t.localScale *= _uniformScale;
            }
            else
            {
                t.localScale = Vector3.Scale(t.localScale, _deltaScale);
            }
        }
    }

    private void AlignSelectedObjects(int axis)
    {
        if (Selection.transforms.Length < 2)
        {
            Debug.LogWarning("At least two objects must be selected.");
            return;
        }
        Undo.RecordObjects(Selection.transforms, $"Align on Axis {axis}");
        float firstPosition = Selection.transforms[0].position[axis];
        for (int i = 1; i < Selection.transforms.Length; i++)
        {
            Vector3 pos = Selection.transforms[i].position;
            pos[axis] = firstPosition;
            Selection.transforms[i].position = pos;
        }
        
        
    }

    private void CenterAlingSelectedObjects()
    {
        if (Selection.transforms.Length < 2)
        {
            Debug.LogWarning("At least two objects must be selected.");
            return;
        }
        Undo.RecordObjects(Selection.transforms, "Center Align");
        Vector3 center = Vector3.zero;
        foreach (Transform t in Selection.transforms)
        {
            center += t.position;
        }
        center /= Selection.transforms.Length;
        foreach (Transform t in Selection.transforms)
        {
            t.position = center;
        }
    }
}
