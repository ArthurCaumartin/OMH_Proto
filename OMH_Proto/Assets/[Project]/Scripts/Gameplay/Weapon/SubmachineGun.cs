using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : Weapon
{
    [Header("SubmachineGun Main Fire Modifier :")]
    [SerializeField] private FloatReference _spreadAngle;

    [SerializeField] private int _maxHeatBullets = 10;
    private float _heatMultiplier = 1f;
    private float _heatTimerCooldown = 0f;
    private int _bulletsHeat;

    public override void Attack()
    {
        base.Attack();

        //TODO Ajouter un système de surchauffe
        _bulletsHeat++;
        if (_bulletsHeat > _maxHeatBullets) _bulletsHeat = _maxHeatBullets;
        _heatMultiplier = _heatMultiplier + _bulletsHeat / _maxHeatBullets;

        Projectile newProj = Instantiate(_projectile, transform.position, transform.rotation);
        newProj.Initialize(_parentShooter, _stat.projectileSpeed.Value, _stat.damage.Value);

        float randomAngle = Random.Range(-_spreadAngle.Value, _spreadAngle.Value);
        float x = Mathf.InverseLerp(-45, 45, randomAngle);
        float angleValue = Mathf.Lerp(-1, 1, x);
        Vector3 newOrientation = new Vector3(angleValue, 0, 1);

        newOrientation = newOrientation.normalized;
        newProj.transform.forward = transform.rotation * newOrientation;
    }

    public override void SecondaryAttack()
    {
        base.SecondaryAttack();
        Projectile newProj = Instantiate(_secondaryProjectile, transform.position, transform.rotation);
        newProj.Initialize(_parentShooter, _secondaryStat.projectileSpeed.Value, _secondaryStat.damage.Value);
    }
}
