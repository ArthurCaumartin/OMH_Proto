using UnityEngine;

public class StateMachine_Pterarmure : StateMachine_MobBase
{
    [SerializeField] private State_Mob_PrepCharge _prepareCharge = new State_Mob_PrepCharge();
    [SerializeField] private State_Mob_Charge _chargeState = new State_Mob_Charge();
    [SerializeField] private State_Mob_AttackArmor _attackState = new State_Mob_AttackArmor();
    [SerializeField] private State_Mob_ChargeAttack _chargeAttackState = new State_Mob_ChargeAttack();

    public State_Mob_PrepCharge PrepChargeState { get => _prepareCharge; }
    public State_Mob_Charge ChargeState { get => _chargeState; }
    public State_Mob_AttackArmor AttackState { get => _attackState; }
    public State_Mob_ChargeAttack ChargeAttackState { get => _chargeAttackState; }

    private void Start()
    {
        _prepareCharge.Initialize(this);
        _chargeState.Initialize(this);
        _attackState.Initialize(this);
        _chargeAttackState.Initialize(this);

        SetState(PrepChargeState);
    }
}
