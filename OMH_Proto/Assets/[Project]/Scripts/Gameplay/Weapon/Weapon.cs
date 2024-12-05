using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


/// <summary>
/// transform.forward set by playerControler with parent rotation
/// </summary>
public class Weapon : MonoBehaviour
{
    //TODO debug le fait de tirer quand on place un Placable, avec OnEnable/Disable et check si on arreter de shoot avant de pouvoir shoot
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
        if (_playerExteralMoveSpeed)
        {
            SetPlayerMoveSpeedMult();
        }

        _attackTime += Time.deltaTime;
        _secondaryDynamicCoolDown.Value += Time.deltaTime;

        if (_attackTime > 1 / _stat.attackPerSecond.Value)
        {
            if (_attackInputAction.ReadValue<float>() > .5f)
            {
                _attackTime = 0;
                Attack();
            }
        }

        if (_secondaryAttackInputAction.ReadValue<float>() > .5f && _secondaryDynamicCoolDown.Value > _secondaryCooldown.Value)
        {
            _secondaryDynamicCoolDown.Value = 0;
            SecondaryAttack();
        }
    }

    private void SetPlayerMoveSpeedMult()
    {
        _playerExteralMoveSpeed.Value = _attackInputAction.ReadValue<float>() > .5f ? _playerMoveSpeedModifier.Value : 1;
    }
}
