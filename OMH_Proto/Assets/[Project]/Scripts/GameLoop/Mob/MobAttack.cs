using UnityEngine;
using UnityEngine.Events;

public class MobAttack : MonoBehaviour
{
    [SerializeField] private TargetFinder _targetFinder;
    [Space]
    [SerializeField] private FloatReference _distanceTrigger;
    [SerializeField] private FloatReference _damage;
    [SerializeField] private FloatReference _attackPerSecond;
    private float _attackTime;
    private MobAnimation _animation;
    private PhysicsAgent _agent;

    private void Start()
    {
        _agent = GetComponent<PhysicsAgent>();
        _animation = GetComponentInChildren<MobAnimation>();
        _animation.resetAfterAnimation = ResetMob;
        _animation.doDamage = DoDamage;
    }

    private void Update()
    {
        if (!_targetFinder.Target) return;
        if (_targetFinder.TargetDistance < _distanceTrigger.Value)
        {
            _attackTime += Time.deltaTime;
            if (_attackTime > 1 / _attackPerSecond.Value)
            {
                _attackTime = 0;
                Attack();
            }
        }
    }

    public void Attack()
    {
        //! play une animation
        //! disable le Physic Agent
        //! donne une Methode de callback
        _animation.Attack();
        _agent.enabled = false;
    }

    private void ResetMob()
    {
        _agent.enabled = true;
    }

    private void DoDamage()
    {
        GameObject target = _targetFinder.Target;
        if (!target) return;
        print("Mob attack : " + target.name);
        Health h = target.GetComponent<Health>();
        if (h) h.health.Value -= _damage.Value;
    }
}