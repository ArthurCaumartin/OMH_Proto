using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BalanceProfile)), CanEditMultipleObjects]
public class BalanceProfileEditor : Editor
{
    public bool verifBakeConst = false;
    public bool verifBakeVariable = false;
    public override void OnInspectorGUI()
    {
        GUILayout.Space(10);
        BalanceProfile balanceProfile = target as BalanceProfile;
        if (GUILayout.Button("Copy Template")) balanceProfile.CopyTemplate();
        GUILayout.Space(10);
        base.OnInspectorGUI();
        GUILayout.Space(10);
        BakeVariable(balanceProfile);
        GUILayout.Space(10);
        BakeConst(balanceProfile);
    }

    private void BakeVariable(BalanceProfile balanceProfile)
    {
        if (!verifBakeVariable)
        {
            if (GUILayout.Button("Bake Variable with Constant"))
            {
                verifBakeVariable = true;
            }
        }
        else
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Sure ?"))
            {
                verifBakeVariable = false;
                balanceProfile.BakeValues();
            }
            if (GUILayout.Button("Cancel"))
                verifBakeVariable = false;
            GUILayout.EndHorizontal();
        }
    }
    private void BakeConst(BalanceProfile balanceProfile)
    {
        if (!verifBakeConst)
        {
            if (GUILayout.Button("Bake Constant With Variable"))
            {
                verifBakeConst = true;
            }
        }
        else
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Sure ?"))
            {
                verifBakeConst = false;
                balanceProfile.BakeConstantWithVariable();
            }
            if (GUILayout.Button("Cancel"))
                verifBakeConst = false;
            GUILayout.EndHorizontal();
        }
    }
}


