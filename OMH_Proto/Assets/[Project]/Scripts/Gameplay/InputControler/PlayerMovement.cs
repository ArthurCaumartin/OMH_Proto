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

        _velocityTarget = new Vector3(_inputVector.x, 0, _inputVector.y)
                         * (_weaponControler.IsShooting() ? _walkMoveSpeed.Value : _runMoveSpeed.Value)
                         * _upgradeMoveSpeedMult;

        Vector3 dirCamToPlayer = (transform.position - _mainCam.transform.position).normalized;
        dirCamToPlayer.y = 0;
        _velocityTarget = Quaternion.LookRotation(dirCamToPlayer, Vector3.up) * _velocityTarget;
        _rb.velocity = _velocityTarget;
    }

    private void OnDisable()
    {
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
