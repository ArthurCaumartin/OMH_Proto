using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGatling : TurretCannon
{
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

    private int _bulletCounter = 0;
    private float _counterDecrease;
    
    public void Update()
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
        
        Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.LookRotation(newDirection, Vector3.up));
        newProjectile.Initialize(_stat.projectileSpeed.Value, _stat.damage.Value * _damagesMultiplier);

        _bulletCounter++;
        _counterDecrease = 0;
    }
    
    protected override void ComputeShootTime()
    {
        float fullShootSpeedMultiplier = Mathf.Lerp(_attackSpeedAtMinCharge, _attackSpeedAtMaxCharge, Mathf.InverseLerp(0, _bulletsMaxCharge, _bulletCounter));
        
        _shootTime += Time.deltaTime;
        print( 1 / (_stat.attackPerSecond.Value * (_attackSpeedMultiplier * fullShootSpeedMultiplier)));
        if (_shootTime > 1 / (_stat.attackPerSecond.Value * (_attackSpeedMultiplier * fullShootSpeedMultiplier)))
        {
            _shootTime = 0;
            Shoot();
        }
    }
}
