using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BalanceProfile)), CanEditMultipleObjects]
public class BalanceProfileEditor : Editor
{
    public bool verif = false;
    public override void OnInspectorGUI()
    {
        GUILayout.Space(10);
        BalanceProfile balanceProfile = target as BalanceProfile;
        if (GUILayout.Button("Copy Template")) balanceProfile.CopyTemplate();
        GUILayout.Space(10);
        base.OnInspectorGUI();
        if (GUILayout.Button("Bake Values")) balanceProfile.BakeValues();
        GUILayout.Space(10);
        GUILayout.Space(10);
        GUILayout.Space(10);
        if (!verif)
        {
            if (GUILayout.Button("Set Constant With Variable"))
            {
                verif = true;
            }
        }
        else
        {
            if (GUILayout.Button("Sure ?"))
            {
                verif = false;
                balanceProfile.BakeConstantWithVariable();
            }
        }
    }
}


