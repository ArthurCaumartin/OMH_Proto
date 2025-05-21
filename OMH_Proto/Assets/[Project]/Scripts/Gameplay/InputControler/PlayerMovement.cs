using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Upgradable
{
    [SerializeField] private UpgradeMeta _moveSpeedUpgrade;
    [Space]
    [SerializeField] private WeaponControler _weaponControler;
    [Header("Movement :")]
    [SerializeField] private FloatReference _runMoveSpeed;
    [SerializeField] private FloatReference _walkMoveSpeed;
    [SerializeField] private FloatReference _acceleration;
    private InputAction _moveInputAction;
    private Rigidbody _rb;
    private Vector2 _inputVector;
    private Vector3 _velocityTarget;
    private Camera _mainCam;
    public float _upgradeMoveSpeedMult = 1;

    public bool IsMoving { get => _inputVector.magnitude != 0; }

    private void Start()
    {
        _mainCam = Camera.main;
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

        Vector3 camForward = _mainCam.transform.forward;
        Vector3 camRight = _mainCam.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;

        Vector3 direction = camForward * _inputVector.y + camRight * _inputVector.x;
        direction = direction.normalized;

        _velocityTarget = direction
                         * (_weaponControler.IsShooting() ? _walkMoveSpeed.Value : _runMoveSpeed.Value)
                         * _upgradeMoveSpeedMult;

        _rb.velocity = _velocityTarget;
    }

    private void OnDisable()
    {
        if(!_rb) return;
        _velocityTarget = Vector3.zero;
        _rb.velocity = Vector3.zero;
    }

    public Vector3 GetMovementDirection()
    {
        return _velocityTarget;
    }

    public override void UpdateUpgrade()
    {
        base.UpdateUpgrade();
        _upgradeMoveSpeedMult = _moveSpeedUpgrade.GetUpgradeValue();
    }
}
