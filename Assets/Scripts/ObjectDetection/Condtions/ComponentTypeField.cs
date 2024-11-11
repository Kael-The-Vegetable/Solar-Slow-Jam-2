using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComponentTypeField
{
    [SerializeField, Tooltip("Select a component type by setting this field.")] private Component selectComponent;

    [SerializeField] private System.Type type;
    public System.Type Type { get => type; set => type = value; }
}
