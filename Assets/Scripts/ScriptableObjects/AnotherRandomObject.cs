using System;
using UnityEngine;

namespace ScriptableObjects
{
    public class AnotherRandomObject : MonoBehaviour
    {
        [SerializeField] private ScriptableEvent _onClickEvent;

        private void OnEnable()
        {
            _onClickEvent.OnEvent.AddListener(AnotherRandomObjectClicked);
        }

        private void AnotherRandomObjectClicked()
        {
            Debug.Log(nameof(AnotherRandomObjectClicked));
        }
    }
}