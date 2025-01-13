using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FlaotReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //! Begin et End c'est pour la gestion des ctrl + z
        EditorGUI.BeginProperty(position, label, property);

        //! get les prop interne
        var propertyType = property.FindPropertyRelative("propertyType");
        var constantValue = property.FindPropertyRelative("constantValue");
        var variable = property.FindPropertyRelative("variable");

        if (propertyType == null || constantValue == null || variable == null)
        {
            EditorGUI.LabelField(position, label.text, "Check FloatReference fields");
            EditorGUI.EndProperty();
            return;
        }

        Rect labelRect = new Rect(position.x, position.y, position.width / 3, position.height);
        Rect popupRect = new Rect(position.x, position.y, position.width, position.height);
        Rect fieldRect = new Rect(position.width / 2f, position.y, position.width / 2, position.height);

        //! tres cryptique la gestion des event encore
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

        if (propertyType.enumValueIndex == (int)PropertyType.Constant)
        {
            EditorGUI.BeginProperty(fieldRect, label, property);
            EditorGUI.DrawRect(position, new Color(0.1f, 0.2f, 0.2f, .35f));

            //! dessine un float field et renvoie la valeur drag
            constantValue.floatValue = EditorGUI.FloatField(position, label, constantValue.floatValue);
        }
        else
        {
            //! ajoute le label de la propetry
            EditorGUI.LabelField(labelRect, label);
            EditorGUI.DrawRect(position, new Color(0.1f, 0.2f, 0.2f, .35f));
            EditorGUI.PropertyField(fieldRect, variable, GUIContent.none);
        }
        // EditorGUI.PropertyField(fieldRect, propertyType.enumValueIndex == (int)PropertyType.Constant ? constantValue : variable, GUIContent.none);
        EditorGUI.EndProperty();
    }
}
