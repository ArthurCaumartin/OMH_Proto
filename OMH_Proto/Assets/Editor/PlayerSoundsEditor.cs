using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerSounds))]
public class PlayerSoundsEditor : Editor
{
    #region SerializeProperties
    SerializedProperty _SoundShieldUp;
    SerializedProperty _SoundShieldDown;

    bool playerShieldGroup = false;
    #endregion
    private void OnEnable()
    {
        _SoundShieldUp = serializedObject.FindProperty("_SoundShieldUp");
        _SoundShieldDown = serializedObject.FindProperty("_SoundShieldDown");
    }
    public override void OnInspectorGUI()
    {
       serializedObject.Update();

        playerShieldGroup = EditorGUILayout.BeginFoldoutHeaderGroup(playerShieldGroup, "Shield sounds");
            if (playerShieldGroup)
            {
                EditorGUILayout.PropertyField(_SoundShieldUp);
                EditorGUILayout.PropertyField(_SoundShieldDown);
            }
        EditorGUILayout.EndFoldoutHeaderGroup();        

       serializedObject.ApplyModifiedProperties();

    }
}
