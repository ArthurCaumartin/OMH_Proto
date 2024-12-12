using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField] private Weapon _currentWeapon;
    [Header("Movement :")]
    [SerializeField] private FloatReference _runMoveSpeed;
    [SerializeField] private FloatReference _walkMoveSpeed;
    [SerializeField] private FloatReference _acceleration;

    [Header("Dash :")]
    [SerializeField] private FloatReference _dashLenght;
    [SerializeField] private FloatReference _dashDuration;
    private InputAction _moveInputAction;
    private Rigidbody _rb;
    private Vector2 _inputVector;
    private Vector3 _velocityTarget;
    private Vector3 _targetVelocitySmoothDamp;

    private bool _isDashing = false;
    private bool _canDash = true;


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
        if (_isDashing) return;
        _inputVector = _moveInputAction.ReadValue<Vector2>();
        _velocityTarget = new Vector3(_inputVector.x, 0, _inputVector.y)
                         * (_currentWeapon.IsPlayerShooting() ? _walkMoveSpeed.Value : _runMoveSpeed.Value);

        //! reach la target c en putain d'option ?
        // _rb.velocity = Vector3.Lerp(_rb.velocity, _velocityTarget, Time.fixedDeltaTime * _acceleration.Value);
        // _rb.velocity = Vector3.SmoothDamp(_rb.velocity, _velocityTarget, ref _targetVelocitySmoothDamp, 1 / _acceleration.Value, 1000, Time.fixedDeltaTime);

        _rb.velocity = _velocityTarget;
    }

    private void Dash()
    {
        _isDashing = true;
        Vector3 dashDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
        DOTween.To((time) =>
        {
            _rb.velocity = dashDirection * (_dashLenght.Value / _dashDuration.Value);
        }, 0, 1, _dashDuration.Value)
        .SetUpdate(UpdateType.Fixed)
        .OnComplete(() => _isDashing = false);
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

    private void OnDash(InputValue value)
    {
        Dash();
    }

    private void OnDrawGizmos()
    {
        if (DEBUG) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, new Vector3(_inputVector.x, 0, _inputVector.y) * _dashLenght.Value);
    }
}
