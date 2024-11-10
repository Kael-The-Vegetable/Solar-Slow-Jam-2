using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    public class RandomObject : MonoBehaviour
    {
     [FormerlySerializedAs("OnClickEvent")] [SerializeField]   private ScriptableEvent _onClickEvent;


        private void OnEnable()
        {
            _onClickEvent.OnEvent.AddListener(RandomActionThing);
        }


        private void RandomActionThing()
        {
            Debug.Log($"{nameof(RandomActionThing)}");
        }
    }
}