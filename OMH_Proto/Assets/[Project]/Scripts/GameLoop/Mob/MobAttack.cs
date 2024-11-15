using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MobAttack : MonoBehaviour
{
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private Transform _attackTransform;
    [SerializeField] private AnimatorParametreSetter _animatorSetter;
    [Space]
    [SerializeField] private FloatReference _distanceTrigger;
    [SerializeField] private FloatReference _damage;
    [SerializeField] private FloatReference _attackPerSecond;
    private float _attackTime;

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
        _animatorSetter.SetParametre();
    }
}
