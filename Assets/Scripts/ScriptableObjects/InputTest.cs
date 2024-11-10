using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace ScriptableObjects
{
    public class InputTest : MonoBehaviour
    {
        [SerializeField] private ScriptableEvent _onClickEvent;


        [SerializeField] private ScriptableVector3Event _onV3Event;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _onClickEvent.OnEvent.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                _onV3Event.OnEvent.Invoke(new Vector3(Random.value, Random.value, Random.value));
            }
        }


        public void LoadLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}