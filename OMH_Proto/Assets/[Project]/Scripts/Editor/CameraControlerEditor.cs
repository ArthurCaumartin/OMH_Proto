using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraControler)), CanEditMultipleObjects]
public class CameraControlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CameraControler controler = (CameraControler)target;
        base.OnInspectorGUI();
        GUILayout.Space(3);
        if (GUILayout.Button("Look At Target")) controler.LookAtTarget();
        GUILayout.Space(3);
        if (GUILayout.Button("Save Offset")) controler.SaveOffSet();
        GUILayout.Space(10);
        if (GUILayout.Button("Reset")) controler.Reset();
    }
}
