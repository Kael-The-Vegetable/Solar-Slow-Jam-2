using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using EditorAttributes;

namespace JDoddsNAIT.ObjectDetection
{
    [System.Serializable]
    public abstract class Conditions
    {
        public enum Types
        {
            None = 0,
            CompareTag = 1,
            HasComponent = 2,
        }

        public static implicit operator Condition.Evaluation(Conditions value)
        {
            return value.Evaluate;
        }

        protected abstract bool Evaluate(GameObject obj);

        [System.Serializable]
        public class None : Conditions
        {
            [SerializeField] private bool returnValue = true;
            protected override bool Evaluate(GameObject gameObject)
            {
                return returnValue;
            }
        }

        [System.Serializable]
        public class CompareTag : Conditions
        {
            [SerializeField, TagDropdown] private string tag;
            protected override bool Evaluate(GameObject obj)
            {
                return obj.CompareTag(tag);
            }
        }

        [System.Serializable]
        public class HasComponent : Conditions
        {
            [SerializeField] private ComponentTypeField component;
            protected override bool Evaluate(GameObject obj)
            {
                return obj.TryGetComponent(component.Type, out _);
            }
        }
    }
}
