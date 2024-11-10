using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JDoddsNAIT.ObjectDetection.Editor
{
    [CustomPropertyDrawer(typeof(ConditionList))]
    public class ConditionListDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty list = property.FindPropertyRelative("conditions");
            return EditorGUI.GetPropertyHeight(list, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty list = property.FindPropertyRelative("conditions");

            for (int i = 0; i < list.arraySize; i++)
            {
                var hideOperator = list.GetArrayElementAtIndex(i).FindPropertyRelative("hideOperator");

                hideOperator.boolValue = i == list.arraySize - 1;
            }

            EditorGUI.BeginProperty(position, label, list);
            EditorGUI.PropertyField(position, list, label);
            EditorGUI.EndProperty();
        }
    }
}