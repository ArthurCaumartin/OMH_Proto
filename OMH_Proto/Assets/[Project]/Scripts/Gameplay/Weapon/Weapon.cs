using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// transform.forward set by playerControler with parent rotation
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private FloatVariable _playerExteralMoveSpeed;
    [Tooltip("Multiplie PlayerMoveSpeed by indicate facotr.")]
    [SerializeField] private FloatReference _playerMoveSpeedModifier;
    [Header("Main Fire : ")]
    [SerializeField] protected Projectile _projectile;
    [SerializeField] protected StatContainer _stat;

    [Header("Secondary Fire :")]
    [SerializeField] protected Projectile _secondaryProjectile;
    [SerializeField] private FloatReference _secondaryCooldown;
    [SerializeField] private FloatReference _secondaryDynamicCoolDown;
    [SerializeField] protected StatContainer _secondaryStat;
    private float _attackTime;
    private InputAction _attackInputAction;
    private InputAction _secondaryAttackInputAction;
    private bool _isAttacking;
    private bool _isSecondaryAttacking;

    private void Start()
    {
        _attackInputAction = GetComponentInParent<PlayerInput>().actions.FindAction("Attack");
        _secondaryAttackInputAction = GetComponentInParent<PlayerInput>().actions.FindAction("SecondaryAttack");
        _secondaryDynamicCoolDown.Value = _secondaryCooldown.Value;
    }

    public virtual void Attack()
    {
        print("Piou piou");
    }

    public virtual void SecondaryAttack()
    {
        print("Secondary piou secondary piou");
    }

    private void Update()
    {
        _isAttacking = _attackInputAction.ReadValue<float>() > .5f;
        _isSecondaryAttacking = _secondaryAttackInputAction.ReadValue<float>() > .5f;

        if (_playerExteralMoveSpeed)
        {
            SetPlayerMoveSpeedMult();
        }

        _attackTime += Time.deltaTime;
        _secondaryDynamicCoolDown.Value += Time.deltaTime;

        if (_attackTime > 1 / _stat.attackPerSecond.Value)
        {
            if (_isAttacking)
            {
                _attackTime = 0;
                Attack();
            }
        }

        if (_isSecondaryAttacking && _secondaryDynamicCoolDown.Value > _secondaryCooldown.Value)
        {
            _secondaryDynamicCoolDown.Value = 0;
            SecondaryAttack();
        }
    }

    private void SetPlayerMoveSpeedMult()
    {
        _playerExteralMoveSpeed.Value = _attackInputAction.ReadValue<float>() > .5f ? _playerMoveSpeedModifier.Value : 1;
    }

    public bool IsPlayerShooting()
    {
        return _isAttacking || _isSecondaryAttacking;
    }
}
