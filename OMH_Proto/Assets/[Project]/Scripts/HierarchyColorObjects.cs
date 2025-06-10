using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// #if UNITY_EDITOR
// using UnityEditor;
// [UnityEditor.InitializeOnLoad]
// #endif
public class HierarchyColorObjects : MonoBehaviour
{
    private static Vector2 offset = new Vector2(20, 1);

    // static HierarchyColorObjects()
    // {
    //     EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    // }

    // private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    // {
    //
    //     var obj = EditorUtility.InstanceIDToObject(instanceID);
    //     if (obj != null)
    //     {
    //         Color backgroundColor = Color.white;
    //         Color textColor = Color.white;
    //         Texture2D texture = null;
    //
    //         // Write your object name in the hierarchy.
    //         if (obj.name == "--- AUDIO ---")
    //         {
    //             backgroundColor = new Color(1f, 0.6470588f, 0f);
    //             textColor = new Color(0.2f, 0.2f, 0.2f);
    //         }
    //         if (obj.name == "AudioThings")
    //         {
    //             backgroundColor = new Color(1f, 0.6470588f, 0f);
    //             textColor = new Color(0.2f, 0.2f, 0.2f);
    //         }
    //         // Or you can use switch case
    //         //switch (obj.name)
    //         //{
    //         //    case "Main Camera":
    //         //        backgroundColor = Color.red;
    //         //        textColor = new Color(0.9f, 0.9f, 0.9f);
    //         //        break;
    //         //}
    //
    //
    //         if (backgroundColor != Color.white)
    //         {
    //             Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
    //             Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);
    //
    //             EditorGUI.DrawRect(bgRect, backgroundColor);
    //             EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
    //             {
    //                 normal = new GUIStyleState() { textColor = textColor },
    //                 fontStyle = FontStyle.Bold
    //             }
    //             );
    //
    //             if (texture != null)
    //                 EditorGUI.DrawPreviewTexture(new Rect(selectionRect.position, new Vector2(selectionRect.height, selectionRect.height)), texture);
    //         }
    //     }
    // }
}
