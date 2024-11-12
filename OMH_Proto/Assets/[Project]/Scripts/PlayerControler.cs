using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [Header("Movement :")]
    [SerializeField] private float _moveSpeed = 15;
    [Header("Dash :")]
    [SerializeField] private float _dashDuration = 3;
    [SerializeField] private float _dashSpeed = 3;
    [SerializeField] private float _dashCooldown = 2;
    [SerializeField] private AnimationCurve _dashVelocityCurve;
    [Header("Aim :")]
    [SerializeField] private CameraControler _camControler;
    [SerializeField] private Transform _aimContainer;
    [SerializeField] private float _aimSpeed = 5;
    private InputAction _moveInputAction;
    private InputAction _aimInputAction;
    private Rigidbody _rb;
    private Vector2 _currentAimTarget;
    private Vector2 _aimVelocity;
    private bool _canMove = true;
    private bool _canAim = true;
    private bool _canDash = true;
    private bool _isDashing = false;
    private Vector3 _dashDirection;
    private Vector2 _inputVector;
    private float _dashTime;

    private void Start()
    {
        _moveInputAction = GetComponent<PlayerInput>().actions.FindAction("Move");
        _aimInputAction = GetComponent<PlayerInput>().actions.FindAction("Aim");
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Move();
        Aim();
        Dash();
    }

    private void Move()
    {
        if (!_canMove) return;

        _inputVector = _moveInputAction.ReadValue<Vector2>();
        _rb.velocity = new Vector3(_inputVector.x, 0, _inputVector.y) * _moveSpeed * 100 * Time.fixedDeltaTime;
    }



    private void Aim()
    {
        if (!_canAim) return;

        Vector2 aimInput = _aimInputAction.ReadValue<Vector2>();
        if (aimInput == Vector2.zero) return;
        _currentAimTarget = Vector2.SmoothDamp(_currentAimTarget, aimInput, ref _aimVelocity, 1 / _aimSpeed, Mathf.Infinity);
        _camControler.SetInputOffSet(_currentAimTarget);
        _aimContainer.forward = new Vector3(_currentAimTarget.x, 0, _currentAimTarget.y);
    }

    private void Dash()
    {
        if (!_isDashing) return;
        _dashTime += Time.deltaTime;
        _rb.velocity = _dashDirection.normalized * 100
                                                 * _dashSpeed
                                                 * _dashVelocityCurve.Evaluate(Mathf.InverseLerp(0, _dashDuration, _dashTime))
                                                 * Time.fixedDeltaTime;

        if (_dashTime > _dashDuration)
        {
            _canMove = true;
            _canAim = true;
            _isDashing = false;
            _dashTime = 0;

            StartCoroutine(DashCooldown(_dashCooldown));
        }
    }

    private void OnDash(InputValue value)
    {
        if (_isDashing || !_canDash) return;

        _canMove = false;
        _canAim = false;

        _isDashing = true;
        _canDash = false;
        _dashDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
    }

    private IEnumerator DashCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        _canDash = true;
    }
}
