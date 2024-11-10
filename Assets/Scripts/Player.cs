using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float MovementForce = 2f;
    public float MaxVelocity = 10;


    [SerializeField] private InputEvents _inputEvents;

    private Rigidbody _rigidbody;
    
    

    /// <summary>
    /// the current movement direction held
    /// </summary>
    private Vector2 movement;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _inputEvents.OnMove.AddListener(OnMove);


        _inputEvents.OnJump.AddListener(OnJump);
    }

    private void OnDisable()
    {
        _inputEvents.OnMove.RemoveListener(OnMove);
    }


    private void OnJump()
    {
        _rigidbody.AddForce(0, 10, 0, ForceMode.Impulse);
    }

    private void OnMove(Vector2 obj)
    {
        movement = obj;

        //  Debug.Log(movement);
    }


    /// <summary>
    /// shitty temp field to avoid calling new() every physics tick
    /// </summary>
    private Vector3 _force = Vector3.zero;

    [FormerlySerializedAs("SmoothTime")] public float StoppingSmoothTime = 1;


    /// <summary>
    /// used by smoothdamp
    /// </summary>
    private Vector3 stoppingSpeed = Vector3.zero;


    // Update is called once per frame
    void FixedUpdate()
    {
        // simple physics based character movement with adjustable stopping time. 
        // Its not perfect as pressing left then right quickly wont cause any velocity damping
        // making moving opposite directions feel more sluggish
        // but we can use this time to rotate the character model the correct way

        // most of the issues can be solved by editing the velocity directly instead of add force
        if (movement.magnitude == 0 && _rigidbody.linearVelocity.magnitude >= 0.000001f)
        {
            //apply stopping force
            var vel = _rigidbody.linearVelocity;
            _rigidbody.linearVelocity = Vector3.SmoothDamp(vel, Vector3.zero, ref stoppingSpeed, StoppingSmoothTime);
        }
        else
        {
            _force.x = movement.x;
            _force.z = movement.y;
            _rigidbody.AddForce(_force.normalized * MovementForce, ForceMode.Acceleration);

            _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, MaxVelocity);
        }

        _force = Vector3.zero;
    }
}