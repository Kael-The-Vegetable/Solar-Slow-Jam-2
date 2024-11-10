using System;
using UnityEngine;
using UnityEngine.Events;

namespace JDoddsNAIT.ObjectDetection
{
    [Serializable]
    public class DetectionEvents
    {
        [SerializeField] private UnityEvent<GameObject> onEnterDetection;
        public UnityEvent<GameObject> OnEnterDetection { get => onEnterDetection; set => onEnterDetection = value; }

        [SerializeField] private UnityEvent<GameObject> onExitDetection;
        public UnityEvent<GameObject> OnExitDetection { get => onExitDetection; set => onExitDetection = value; }

    }
}