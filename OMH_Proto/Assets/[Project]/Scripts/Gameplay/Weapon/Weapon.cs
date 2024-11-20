using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// transform.forward set by playerControler with parent rotation
/// </summary>
public class Weapon : MonoBehaviour
{
    //TODO debug le fait de tirer quand on place un Placable, avec OnEnable/Disable et check si on arreter de shoot avant de pouvoir shoot
    [SerializeField] protected Projectile _projectile;
    [SerializeField] protected StatContainer _stat;
    [Space]
    [SerializeField] private FloatVariable _moveSpeed;
    [SerializeField] private FloatReference _moveSpeedModifier;
    private float _attackTime;
    private InputAction _attackInputAction;
    private float _startMs;

    private void Start()
    {
        _attackInputAction = GetComponentInParent<PlayerInput>().actions.FindAction("Attack");
        if (_moveSpeed) _startMs = _moveSpeed.Value;
    }

    public virtual void Attack()
    {
        print("Piou piou");
    }

    private void Update()
    {
        if (_moveSpeed)
        {
            _moveSpeed.Value = _startMs + (_attackInputAction.ReadValue<float>() > .5f ? _moveSpeedModifier.Value : 0);
        }

        _attackTime += Time.deltaTime;
        if (_attackInputAction.ReadValue<float>() > .5f && _attackTime > 1 / _stat.attackPerSecond.Value)
        {


            _attackTime = 0;
            Attack();
        }
    }

    // private void OnAttack(InputValue value)
    // {
    //     if (value.Get<float>() > .5f && _attackTime > 1 / _stat.attackPerSecond.Value)
    //     {
    //         _attackTime = 0;
    //         Attack();
    //     }
    // }
}
