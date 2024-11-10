using System;
using UnityEngine;

namespace ScriptableObjects
{
    public class VectorEventTest : MonoBehaviour
    {
        [SerializeField] private ScriptableVector3Event _onV3Event;

        private void OnEnable()
        {
            _onV3Event.OnEvent.AddListener(DoSomethingWithAV3);
        }

        private void OnDisable()
        {
            _onV3Event.OnEvent.RemoveListener(DoSomethingWithAV3);
        }

        private void DoSomethingWithAV3(Vector3 vector3)
        {
            Debug.Log(vector3);
        }
    }
}