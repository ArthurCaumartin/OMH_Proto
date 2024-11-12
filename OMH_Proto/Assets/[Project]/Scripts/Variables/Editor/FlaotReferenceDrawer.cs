using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FlaotReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //! get les prop interne
        var propertyType = property.FindPropertyRelative("propertyType");
        var constantValue = property.FindPropertyRelative("constantValue");
        var variable = property.FindPropertyRelative("variable");

        if (propertyType == null || constantValue == null || variable == null)
        { 
            EditorGUI.LabelField(position, label.text, "Check FloatReference fields");
            EditorGUI.EndProperty(); // Assurez-vous de bien terminer ici aussi
            return;
        }

        Rect labelRect = new Rect(position.x, position.y, position.width / 3, position.height);
        Rect popupRect = new Rect(position.x, position.y, position.width, position.height);
        Rect fieldRect = new Rect(position.width / 2f, position.y, position.width / 2, position.height);
        // Rect fieldRect = new Rect(position.x/*  + 2 * position.width / 4 */, position.y, position.width / 3, position.height);

        EditorGUI.LabelField(labelRect, label);

        // propertyType.enumValueIndex = EditorGUI.Popup(popupRect, propertyType.enumValueIndex, propertyType.enumDisplayNames);

        if (Event.current.type == EventType.ContextClick && popupRect.Contains(Event.current.mousePosition))
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Use Constant"), propertyType.enumValueIndex == (int)PropertyType.Constant, () =>
            {
                propertyType.enumValueIndex = (int)PropertyType.Constant;
                property.serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("Use Variable"), propertyType.enumValueIndex == (int)PropertyType.Variable, () =>
            {
                propertyType.enumValueIndex = (int)PropertyType.Variable;
                property.serializedObject.ApplyModifiedProperties();
            });
            menu.ShowAsContext();
            Event.current.Use();
        }
        EditorGUI.PropertyField(fieldRect, propertyType.enumValueIndex == (int)PropertyType.Constant ? constantValue : variable, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
