using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class Surface : MonoBehaviour
{
    /// <summary>
    /// called every frame the orb hits this surface
    /// </summary>
    [FormerlySerializedAs("OnOrbStay")] public UnityEvent<Surface, Orb> OnOrbHit;

    public UnityEvent<Surface, Orb> OnOrbExit;
    public UnityEvent<Surface, Orb> OnOrbEnter;
    public bool AllowOrbReflection = false;


    private bool _lastFrame = false;
    private bool _currentFrame = false;


    private Orb _orb;

    private void Start()
    {
        OnOrbHit.AddListener(OnHit);
    }

    private void OnHit(Surface surface, Orb orb)
    {
        _orb = orb;
        if (_lastFrame == false && _currentFrame == false)
        {
            OnOrbEnter.Invoke(surface, orb);
        }

        _currentFrame = true;
    }

    public void Test(string test)
    {
        Debug.Log(test);
    }

    private void LateUpdate()
    {
        if (_currentFrame == false && _lastFrame == true)
        {
            //beam stopped hitting us

            OnOrbExit.Invoke(this, _orb);
        }

        if (_currentFrame == false)
        {
            _lastFrame = false;
        }
        else
        {
            _lastFrame = true;
            _currentFrame = false;
        }
    }
}