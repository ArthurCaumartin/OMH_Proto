using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MobAttack : MonoBehaviour
{
    [FormerlySerializedAs("_targetFinder")][SerializeField] private AgentTargetFinder agentTargetFinder;
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
