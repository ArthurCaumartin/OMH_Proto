using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent)), CanEditMultipleObjects]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameEvent e = target as GameEvent;

        GUILayout.Space(5);
        if (GUILayout.Button("Raise")) e.Raise();
        GUILayout.Space(5);
        if (GUILayout.Button("Raise True")) e.Raise(true);
        if (GUILayout.Button("Raise False")) e.Raise(false);
    }
}
