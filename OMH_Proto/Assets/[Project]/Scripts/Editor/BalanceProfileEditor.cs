using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BalanceProfile)), CanEditMultipleObjects]
public class BalanceProfileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(10);
        BalanceProfile balanceProfile = target as BalanceProfile;
        if (GUILayout.Button("Copy Template")) balanceProfile.CopyTemplate();
        GUILayout.Space(10);
        base.OnInspectorGUI();
        if (GUILayout.Button("Bake Values")) balanceProfile.BakeValues();
    }
}


