using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputReader", menuName = "SO/Input/InputReader", order = 0)]
public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _playerInput;
    [SerializeField] private InputEvents _inputEvents;


    public void Setup()
    {
        if (_playerInput is null)
        {
            Debug.Log("Input reader init");
            _playerInput = new InputSystem_Actions();

            _playerInput.Player.SetCallbacks(this);
            _playerInput.Player.Enable();
        }
    }

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        if (_playerInput is not null)
        {
            _playerInput.Player.Disable();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputEvents.OnMove.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _inputEvents.OnLook.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        _inputEvents.OnAttack.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _inputEvents.OnInteract.Invoke();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _inputEvents.OnJump.Invoke();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
    }

    public void OnNext(InputAction.CallbackContext context)
    {
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }
}