using UnityEngine;

public class TurretCannon : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] protected Transform _projectileSpawnPivot;
    [SerializeField] protected Projectile _projectilePrefab;
    [SerializeField] protected TurretTargetFinder _finder;
    [SerializeField] protected StatContainer _stat;
    protected Transform _currentTarget;
    protected float _shootTime = 0;

    [Tooltip("Modify turret attackSpeed")]
    [SerializeField, Range(0.1f, 10)] public float _attackSpeedMultiplier = 1;

    [Tooltip("Modify turret damages per bullet")]
    [SerializeField, Range(0.1f, 10)] public float _damagesMultiplier = 1;

    public virtual void Shoot()
    {
        Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        newProjectile.Initialize(transform.parent.gameObject, _stat.projectileSpeed.Value, _stat.damage.Value * _damagesMultiplier);
    }

    public virtual void Update()
    {
        FindTarget();
        if (_currentTarget)
        {
            LookAtTarget();
            ComputeShootTime();
        }
    }

    private void FindTarget()
    {
        if (!_currentTarget || Vector3.Distance(transform.position, _currentTarget.transform.position) > _stat.range.Value)
        {
            _currentTarget = _finder.GetNearsetMob(_stat.range.Value)?.transform;
        }
        if (!_currentTarget) return;
    }

    protected virtual void ComputeShootTime()
    {
        _shootTime += Time.deltaTime;
        if (_shootTime > 1 / (_stat.attackPerSecond.Value * _attackSpeedMultiplier))
        {
            _shootTime = 0;
            Shoot();
        }
    }

    private void LookAtTarget()
    {
        if (!_currentTarget) return;
        Vector3 lootAt = (_currentTarget.position - transform.position).normalized;
        lootAt.y = 0;
        transform.forward = lootAt;
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }


    public void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(0, 0, 1, .2f);
        if (_stat.range != null) Gizmos.DrawSphere(transform.position, _stat.range.Value);
    }
}
