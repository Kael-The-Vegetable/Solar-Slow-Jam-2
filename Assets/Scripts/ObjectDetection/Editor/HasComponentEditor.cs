using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HasComponentEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
    }

    static float AddRectWidth(Rect rect, float spacing = 0) => rect.x + rect.width + spacing;
    static float AddRectHeight(Rect rect, float spacing = 0) => rect.y + rect.height + spacing;
}
