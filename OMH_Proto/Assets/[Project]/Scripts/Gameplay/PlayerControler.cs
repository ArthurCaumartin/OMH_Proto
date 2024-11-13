using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    //TODO PLAYER Faire un script player Aim / player movement !!!
    public bool DEBUG = false;
    [Header("Movement :")]
    [SerializeField] private FloatReference _moveSpeed;
    [SerializeField] private FloatReference _moveAcceleration;

    [Header("Aim :")]
    [SerializeField] private LayerMask _groundLayer = 10;
    [SerializeField] private CameraControler _camControler;
    [SerializeField] private Transform _aimContainer;
    [SerializeField] private FloatReference _aimSpeed;
    private Vector3 _mouseWorldPos;

    private InputAction _moveInputAction;
    private InputAction _aimInputAction;
    private Rigidbody _rb;
    private Vector2 _currentAimTarget;
    private Vector3 _aimVelocity;
    private bool _canMove = true;
    private bool _canAim = true;
    private bool _canDash = true;
    private bool _isDashing = false;
    private Vector3 _dashDirection;
    private Vector2 _inputVector;
    private float _dashTime;
    private Camera _mainCamera;

    private void Start()
    {
        if (!GetComponent<PlayerInput>() || !GetComponent<PlayerInput>().actions)
        {
            print("NOT PLAYER INPUT ON PLAYER OBJECT");
            enabled = false;
            return;
        }

        _mainCamera = Camera.main;
        _camControler = _mainCamera.GetComponent<CameraControler>();

        _moveInputAction = GetComponent<PlayerInput>().actions.FindAction("Move");
        _aimInputAction = GetComponent<PlayerInput>().actions.FindAction("Aim");
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Move();
        MouseAim();
    }

    private void Move()
    {
        if (!_canMove) return;

        _inputVector = _moveInputAction.ReadValue<Vector2>();
        Vector3 velocityTarget = new Vector3(_inputVector.x, 0, _inputVector.y) * _moveSpeed.Value * 100 * Time.fixedDeltaTime;
        _rb.velocity = Vector3.Lerp(_rb.velocity, velocityTarget, Time.deltaTime * _moveAcceleration.Value);
    }

    /// <summary> 
    /// Get la pos du pointer sur le sol et compute la direction par rapport au joueur
    /// <summary> 
    private void MouseAim()
    {
        if (!_canAim) return;

        Vector2 pixelPos = _aimInputAction.ReadValue<Vector2>();
        Ray camRay = _mainCamera.ScreenPointToRay(pixelPos);

        if (DEBUG) Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.green);

        Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity, _groundLayer);
        if (!hit.collider) return;

        if (DEBUG) Debug.DrawLine(new Vector3(hit.point.x, hit.point.y - 1, hit.point.z)
                                , new Vector3(hit.point.x, hit.point.y + 1, hit.point.z)
                                , Color.red);

        _mouseWorldPos = Vector3.SmoothDamp(_mouseWorldPos, hit.point, ref _aimVelocity, 1 / _aimSpeed.Value, Mathf.Infinity);
        Vector3 worldMouseDirection = (_mouseWorldPos - transform.position).normalized;
        worldMouseDirection.y = 0;

        _camControler.SetInputOffSet(worldMouseDirection);
        _aimContainer.forward = worldMouseDirection;
    }

    // private void ControlerAim()
    // {
    //     if (!_canAim) return;

    //     Vector2 aimInput = _aimInputAction.ReadValue<Vector2>();
    //     if (aimInput == Vector2.zero) return;
    //     _currentAimTarget = Vector2.SmoothDamp(_currentAimTarget, aimInput, ref _aimVelocity, 1 / _aimSpeed, Mathf.Infinity);
    //     _camControler.SetInputOffSet(_currentAimTarget);
    //     _aimContainer.forward = new Vector3(_currentAimTarget.x, 0, _currentAimTarget.y);
    // }

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
