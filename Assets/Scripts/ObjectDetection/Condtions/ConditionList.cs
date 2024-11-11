using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDoddsNAIT.ObjectDetection
{
    [System.Serializable]
    public class ConditionList
    {
        [SerializeField] private List<Condition> conditions;
        public List<Condition> Conditions { get => conditions; set => conditions = value; }

        public bool EvaluateAll(GameObject gameObject)
        {
            bool returnValue = true;

            List<bool> passed = new();

            if (conditions.Count > 0)
            {
                List<Condition> conditionBlock = new();
                for (int i = 0; i < Conditions.Count; i++)
                {
                    Condition c = Conditions[i];
                    if (c.Operator == Condition.LogicOperator.AND && c.hideOperator == false)
                    {
                        conditionBlock.Add(c);
                        conditionBlock.Add(Conditions[i + 1]);
                    }
                    else if (conditionBlock.Count > 0)
                    {
                        passed.Add(EvaluateList(conditionBlock, gameObject));
                        conditionBlock.Clear();
                    }
                    else
                    {
                        passed.Add(c.Evaluate(gameObject));
                    }
                }
            }

            returnValue = passed.Contains(true);

            return returnValue;
        }

        static bool EvaluateList(List<Condition> conditions, GameObject gameObject)
        {
            int count = 0;
            foreach (var condition in conditions)
            {
                if (condition.Evaluate(gameObject))
                {
                    count++;
                }
            }
            return count == conditions.Count;
        }
    }
}