using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TurretGatling : TurretCannon
{
    [Header("Gatling : ")]
    [Tooltip("How many bullets needed to go max charge")]
    [SerializeField] private int _bulletsMaxCharge = 10;

    [Tooltip("Attack speed multiplier at 0 charge")]
    [SerializeField] private float _attackSpeedAtMinCharge = 0.6f;

    [Tooltip("Attack speed multiplier at max charge")]
    [SerializeField] private float _attackSpeedAtMaxCharge = 1.35f;

    [Tooltip("Angle max where bullets go when at max charge")]
    [SerializeField, Range(0, 90)] private float _angleShootAtMaxCharge = 35;

    [Tooltip("Time between decreasing charge when not firing")]
    [SerializeField] private float _timeToDecreaseCharge = 0.5f;
    [Space]
    [SerializeField] private Transform _hat;
    [SerializeField] private WeaponVisual _weaponVisual;

    private int _bulletCounter = 0;
    private float _counterDecrease;
    private float _fullShootSpeedMultiplier;
    private Vector3 _cannonMeshStartPos;
    private Vector3 _hatMeshStartPos;

    public void Start()
    {
        _cannonMeshStartPos = _canonMeshPivot.localPosition;
        _hatMeshStartPos = _hat.localPosition;
    }

    public override void Update()
    {
        base.Update();

        _counterDecrease += Time.deltaTime;
        if (_counterDecrease >= _timeToDecreaseCharge)
        {
            if (_bulletCounter == 0)
            {
                return;
            }
            _bulletCounter--;
            _counterDecrease = 0;
        }

    }
    public override void Shoot()
    {
        float fullSpreadMultiplier = Mathf.InverseLerp(0, _bulletsMaxCharge, _bulletCounter);

        float tempAngleTime = Mathf.InverseLerp(0, 90, _angleShootAtMaxCharge * fullSpreadMultiplier);
        float tempAngleFloat = Mathf.Lerp(-tempAngleTime, tempAngleTime, Random.value);

        Vector3 newDirection = new Vector3(tempAngleFloat, 0, 1);
        newDirection = transform.rotation * newDirection.normalized;
        newDirection.y = 0;

        Projectile newProjectile = Instantiate(_projectilePrefab, _projectileSpawnPivot.position, Quaternion.LookRotation(newDirection, Vector3.up));
        newProjectile.Initialize(transform.parent.gameObject, _stat.projectileSpeed.Value, _stat.damage.Value * _damagesMultiplier);

        _bulletCounter++;
        _counterDecrease = 0;

        print("Turret triger visual");
        _weaponVisual.PlayVisual(Vector3.one * .8f);
        CannonAnimation();
    }

    protected override void ComputeShootTime()
    {
        _fullShootSpeedMultiplier = Mathf.Lerp(_attackSpeedAtMinCharge, _attackSpeedAtMaxCharge, Mathf.InverseLerp(0, _bulletsMaxCharge, _bulletCounter));

        _shootTime += Time.deltaTime;
        if (_shootTime > 1 / (_stat.attackPerSecond.Value * (_attackSpeedMultiplier * _fullShootSpeedMultiplier)))
        {
            _shootTime = 0;
            Shoot();
        }
    }


    private void CannonAnimation()
    {
        Vector3 dir = _currentTarget.position - transform.position;
        dir = dir.normalized * 0.3f;
        float duration = 1 / (_stat.attackPerSecond.Value * (_attackSpeedMultiplier * _fullShootSpeedMultiplier));
        // duration /= 4;

        duration = Mathf.Clamp(duration, 0, .1f);

        //! .002
        _canonMeshPivot.DOMove(_canonMeshPivot.position - dir, duration / 2)
        .OnComplete(() => { _canonMeshPivot.DOLocalMove(_cannonMeshStartPos, duration / 2); });

        _hat.DOLocalMove(_hatMeshStartPos + (_hat.up * .003f), duration / 4)
        .OnComplete(() => { _hat.DOLocalMove(_hatMeshStartPos, duration / 4); });
    }
}
