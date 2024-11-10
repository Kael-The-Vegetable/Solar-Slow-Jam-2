using System;
using UnityEngine;

namespace JDoddsNAIT.ObjectDetection
{
    [System.Serializable]
    public class Condition
    {
        public delegate bool Evaluation(GameObject obj);

        public enum LogicOperator
        {
            AND = 0, OR = 1
        }

        enum Invert { [InspectorName("_")] False = 0, [InspectorName("NOT")] True = 1 }

        [HideInInspector] public bool hideOperator = false;

        [SerializeField] private LogicOperator @operator;
        public LogicOperator Operator { get => @operator; set => @operator = value; }

        [SerializeField] private Invert invert;
        public bool InvertCondition { get => invert == Invert.True; set => invert = value ? Invert.True : Invert.False; }

        [SerializeField] private Conditions.Types currentCondition;
        public Conditions.Types CurrentCondition { get => currentCondition; set => currentCondition = value; }

        public bool Evaluate(GameObject gameObject)
        {
            GetCurrentConditionName(out var evaluation);
            return InvertCondition ? !evaluation(gameObject) : evaluation(gameObject);
        }

        public string GetCurrentConditionName() => GetCurrentConditionName(out _);
        public string GetCurrentConditionName(out Evaluation evaluation)
        {
            Tuple<string, Evaluation> result = CurrentCondition switch
            {
                Conditions.Types.None => new(nameof(None), None),
                Conditions.Types.CompareTag => new(nameof(CompareTag), CompareTag),
                Conditions.Types.HasComponent => new(nameof(HasComponent), HasComponent),
                _ => new(nameof(None), None),
            };
            evaluation = result.Item2;
            return result.Item1;
        }

        [SerializeField] private Conditions.None None;
        [SerializeField] private Conditions.CompareTag CompareTag;
        [SerializeField] private Conditions.HasComponent HasComponent;
    }
}