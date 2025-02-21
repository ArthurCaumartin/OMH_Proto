using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShaderValueSetter)), CanEditMultipleObjects]
public class ShaderValueSetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(10);
        ShaderValueSetter balanceProfile = target as ShaderValueSetter;
        if (GUILayout.Button("Bake Value")) balanceProfile.SetValue();
    }
}