using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JDoddsNAIT.ObjectDetection.Editor
{
    [CustomPropertyDrawer(typeof(Condition))]
    public class ConditionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Condition self = property.boxedValue as Condition;
            float defaultHeight = EditorGUIUtility.standardVerticalSpacing + (property.FindPropertyRelative("hideOperator").boolValue
                ? 0f
                : (EditorGUIUtility.singleLineHeight));

            float conditionHeight;

            string conditionName = self.GetCurrentConditionName();

            SerializedProperty currentCondition = property.FindPropertyRelative(conditionName);
            conditionHeight = EditorGUI.GetPropertyHeight(currentCondition, true);

            return defaultHeight + conditionHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Condition self = property.boxedValue as Condition;

            bool hideOperator = property.FindPropertyRelative("hideOperator").boolValue;

            float quarterWidth = position.width / 4;

            SerializedProperty invertCondition = property.FindPropertyRelative("invert");
            Rect invertRect = new(
                x: position.x,
                y: position.y,
                width: Mathf.Clamp(quarterWidth, 50f, position.width),
                height: EditorGUI.GetPropertyHeight(invertCondition));

            SerializedProperty conditionName = property.FindPropertyRelative("currentCondition");
            Rect conditionNameRect = new(
                x: AddRectWidth(invertRect),
                y: position.y,
                width: Mathf.Clamp(position.width - quarterWidth, 50f, position.width),
                height: EditorGUI.GetPropertyHeight(conditionName));

            SerializedProperty currentCondition = property.FindPropertyRelative(self.GetCurrentConditionName());
            Rect conditionRect = new(
                x: position.x,
                y: conditionNameRect.y,
                width: position.width,
                height: EditorGUI.GetPropertyHeight(currentCondition, true));

            SerializedProperty logicalOperator = property.FindPropertyRelative("operator");
            Rect operatorRect = new(
                x: position.x,
                y: AddRectHeight(conditionRect, EditorGUIUtility.standardVerticalSpacing),
                width: Mathf.Clamp(quarterWidth, 50f, position.width),
                height: hideOperator
                    ? 0f
                    : EditorGUI.GetPropertyHeight(logicalOperator));

            if (hideOperator)
            {
                self.Operator = Condition.LogicOperator.AND;
            }

            // Draw properties


            EditorGUI.BeginProperty(conditionNameRect, GUIContent.none, conditionName);
            EditorGUI.PropertyField(conditionNameRect, conditionName, GUIContent.none);
            EditorGUI.EndProperty();

            EditorGUI.BeginProperty(invertRect, GUIContent.none, invertCondition);
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(invertRect, invertCondition, GUIContent.none);
            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();

            EditorGUI.BeginProperty(conditionRect, GUIContent.none, currentCondition);
            EditorGUI.PropertyField(conditionRect, currentCondition, GUIContent.none, true);
            EditorGUI.EndProperty();

            if (!hideOperator)
            {
                EditorGUI.BeginProperty(operatorRect, GUIContent.none, logicalOperator);
                EditorGUI.PropertyField(operatorRect, logicalOperator, GUIContent.none);
                EditorGUI.EndProperty();
            }
        }

        static float AddRectWidth(Rect rect, float spacing = 0) => rect.x + rect.width + spacing;
        static float AddRectHeight(Rect rect, float spacing = 0) => rect.y + rect.height + spacing;
    }
}