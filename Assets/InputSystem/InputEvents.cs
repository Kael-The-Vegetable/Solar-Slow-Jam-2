using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


    [CreateAssetMenu(fileName = "InputEvents", menuName = "SO/Input/InputEvents", order = 0)]
    public class InputEvents : ScriptableObject
    {
        [HideInInspector] public UnityEvent<Vector2> OnMove;
        [HideInInspector] public UnityEvent<Vector2> OnLook;
        [HideInInspector] public UnityEvent OnAttack;
        [HideInInspector] public UnityEvent OnInteract;
        [HideInInspector] public UnityEvent OnJump;
    }