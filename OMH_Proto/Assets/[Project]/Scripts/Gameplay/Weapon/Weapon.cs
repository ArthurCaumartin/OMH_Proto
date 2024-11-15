using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    //! /////////////////////////////////////////////////////////////
    //? transform.forward set by playerControler with parent rotation
    //! /////////////////////////////////////////////////////////////
    [SerializeField] protected Projectile _projectile;
    [SerializeField] protected StatContainer _stat;
    private float _attackTime;
    private InputAction _attackInputAction;

    private void Start()
    {
        _attackInputAction = GetComponentInParent<PlayerInput>().actions.FindAction("Attack");
    }

    public virtual void Attack()
    {
        print("Piou piou");
    }

    private void Update()
    {
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
