using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JDoddsNAIT.ObjectDetection
{
    public class ConditionalDetector : ObjectDetector
    {
        [Space]
        [SerializeField] private ConditionList conditions;
        public ConditionList Conditions { get => conditions; set => conditions = value; }

        protected override Func<GameObject, bool> Condition => obj => base.Condition(obj) & Conditions.EvaluateAll(obj);

        [ContextMenu("Test Conditions")]
        private void TestConditions()
        {
            Debug.Log(Conditions.EvaluateAll(gameObject));
        }
    }
}