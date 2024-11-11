using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ComponentTypeField))]
public class ComponentTypeFieldDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 2 * EditorGUI.GetPropertyHeight(property.FindPropertyRelative("selectComponent"));
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty component = property.FindPropertyRelative("selectComponent");
        Rect componentRect = new(position.x, position.y, position.width, EditorGUI.GetPropertyHeight(property, label));

        EditorGUI.ObjectField(componentRect, component);

        var self = property.boxedValue as ComponentTypeField;

        self.Type = component.boxedValue?.GetType();

        EditorGUI.LabelField(new(position.x, AddRectHeight(componentRect), position.width, EditorGUIUtility.singleLineHeight), self.Type == null ? "Null" : self.Type.ToString());
    }

    static float AddRectWidth(Rect rect, float spacing = 0) => rect.x + rect.width + spacing;
    static float AddRectHeight(Rect rect, float spacing = 0) => rect.y + rect.height + spacing;
}
