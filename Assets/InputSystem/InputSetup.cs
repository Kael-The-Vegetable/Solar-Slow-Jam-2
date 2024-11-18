using System;
using UnityEngine;

/// <summary>
/// this is an atempt at preventing some weird editor issues on exactly when the
/// OnEnable method is called in a scriptable object.
/// </summary>
public class InputSetup : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private void Start()
    {
        _inputReader.Setup();
    }
}