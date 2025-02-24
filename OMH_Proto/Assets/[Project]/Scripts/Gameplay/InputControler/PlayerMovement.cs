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
    public float _upgradeMoveSpeedMult = 1;

    public override void Start()
    {
        base.Start();
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
                         * (_weaponControler.IsPlayerShooting() ? _walkMoveSpeed.Value : _runMoveSpeed.Value)
                         * _upgradeMoveSpeedMult;

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

    public override void UpdateUpgrade()
    {
        base.UpdateUpgrade();
        _upgradeMoveSpeedMult = _moveSpeedUpgrade.GetUpgradeValue();
    }
}
