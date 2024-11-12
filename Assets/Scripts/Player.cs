using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float MovementForce = 2f;
    public float JumpVelocity = 5;


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
        if (IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector3(0, JumpVelocity, 0);
        }
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

    public float StoppingSmoothTime = 1;


    /// <summary>
    /// used by smoothdamp
    /// </summary>
    private Vector3 stoppingSpeed = Vector3.zero;


    [SerializeField] private bool IsGrounded;


    // Update is called once per frame
    void FixedUpdate()
    {
        // look into this https://catlikecoding.com/unity/tutorials/movement/surface-contact/
        // at the very least i should try and get ground snapping and aligning the velocity to the surface you are walking on
        // but a working demo is more important. 
        _force.x = movement.x;
        _force.z = movement.y;
        _force.Normalize();

        _force *= MovementForce;
        var vel = _rigidbody.linearVelocity;
        var damp = Vector3.SmoothDamp(vel, _force, ref stoppingSpeed, StoppingSmoothTime);

        damp.y = vel.y;
        _rigidbody.linearVelocity = damp;


        _force = Vector3.zero;
    }

    private void OnCollisionEnter(Collision other)
    {
        IsGrounded = GroundCheck(other);
    }

    private void OnCollisionStay(Collision other)
    {
        IsGrounded = GroundCheck(other);
    }

    private void OnCollisionExit(Collision other)
    {
        IsGrounded = GroundCheck(other);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns true if the collision is ground </returns>
    private bool GroundCheck(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            var normal = collision.GetContact(i).normal;

            if (normal.y >= 0.8f)
            {
                return true;
            }
        }

        return false;
    }
}