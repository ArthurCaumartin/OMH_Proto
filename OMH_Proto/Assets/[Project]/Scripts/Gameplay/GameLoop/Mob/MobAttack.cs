using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MobAttack : MonoBehaviour
{
    //TODO passer la logique de ce script dans State_Mob_Chase ou State_Mob_Attack ?
    [FormerlySerializedAs("_targetFinder")][SerializeField] private MobTargetFinder agentTargetFinder;
    [SerializeField] private MobAnimationControler _animationControler;
    [Space]
    [SerializeField] private FloatReference _distanceTrigger;
    [SerializeField] private FloatReference _attackPerSecond;
    private float _attackTime;

    private void Update()
    {
        if (!agentTargetFinder.Target) return;
        if (agentTargetFinder.TargetDistance <= _distanceTrigger.Value)
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
        _animationControler.PlayAttackAnimation();
    }
}
