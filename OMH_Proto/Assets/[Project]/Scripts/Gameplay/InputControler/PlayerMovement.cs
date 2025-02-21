using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private WeaponControler _weaponControler;
    [Header("Movement :")]
    [SerializeField] private FloatReference _runMoveSpeed;
    [SerializeField] private FloatReference _walkMoveSpeed;
    [SerializeField] private FloatReference _acceleration;
    private InputAction _moveInputAction;
    private Rigidbody _rb;
    private Vector2 _inputVector;
    private Vector3 _velocityTarget;
    private Vector3 _targetVelocitySmoothDamp;


    private void Start()
    {
        if (!GetComponent<PlayerInput>() || !GetComponent<PlayerInput>().actions)
        {
            print("NOT PLAYER INPUT ON PLAYER OBJECT");
            enabled = false;
            return;
        }

        _moveInputAction = GetComponent<PlayerInput>().actions.FindAction("GroundMove");
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        _inputVector = _moveInputAction.ReadValue<Vector2>();
        _velocityTarget = new Vector3(_inputVector.x, 0, _inputVector.y)
                         * (_weaponControler.IsPlayerShooting() ? _walkMoveSpeed.Value : _runMoveSpeed.Value);

        //! reach la target c en putain d'option ?
        // _rb.velocity = Vector3.Lerp(_rb.velocity, _velocityTarget, Time.fixedDeltaTime * _acceleration.Value);
        // _rb.velocity = Vector3.SmoothDamp(_rb.velocity, _velocityTarget, ref _targetVelocitySmoothDamp, 1 / _acceleration.Value, 1000, Time.fixedDeltaTime);

        _rb.velocity = _velocityTarget;
    }

    private void OnDisable()
    {
        _velocityTarget = Vector3.zero;
        _rb.velocity = Vector3.zero;
    }

    public Vector3 GetMovementDirection()
    {
        return _velocityTarget.normalized;
    }
}
