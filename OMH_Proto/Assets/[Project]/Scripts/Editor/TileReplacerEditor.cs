using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileReplacer)), CanEditMultipleObjects]
public class TileReplacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TileReplacer tileRePlacer = target as TileReplacer;
        base.OnInspectorGUI();
        if (GUILayout.Button("Replace Tile"))
        {
            tileRePlacer.RoundPosition();
        }
    }
}
