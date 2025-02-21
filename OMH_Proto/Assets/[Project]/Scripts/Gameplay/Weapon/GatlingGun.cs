using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatlingGun : Weapon
{
    [SerializeField] private FloatReference _gatlingMunitions;
    [SerializeField] private TextMeshProUGUI _gatlingMunitionsText;
    private float _currentGatlingMunition;

    private void Start()
    {
        _currentGatlingMunition = _gatlingMunitions.Value;
    }

    public void GatlingShot()
    {
        _currentGatlingMunition --;
        _gatlingMunitionsText.text = _currentGatlingMunition.ToString();
        if (_currentGatlingMunition <= 0)
        {
            //Destroy Gatling
        }
    }
    public override void Attack()
    {
        base.Attack();

        //TODO Ajouter un système de surchauffe
        
        // _bulletsHeat++;
        // if (_bulletsHeat > _maxHeatBullets) _bulletsHeat = _maxHeatBullets;
        // _heatMultiplier = _heatMultiplier + _bulletsHeat / _maxHeatBullets;
        //
        // Projectile newProj = Instantiate(_projectile, transform.position, transform.rotation);
        // newProj.Initialize(_parentShooter, _stat.projectileSpeed.Value, _stat.damage.Value);
        //
        // float randomAngle = Random.Range(-_spreadAngle.Value, _spreadAngle.Value);
        // float x = Mathf.InverseLerp(-45, 45, randomAngle);
        // float angleValue = Mathf.Lerp(-1, 1, x);
        // Vector3 newOrientation = new Vector3(angleValue, 0, 1);
        //
        // newOrientation = newOrientation.normalized;
        // newProj.transform.forward = transform.rotation * newOrientation;
    }

    public override void SecondaryAttack()
    {
        if (_secondaryProjectile == null) return;
        
        base.SecondaryAttack();
        Projectile newProj = Instantiate(_secondaryProjectile, transform.position, transform.rotation);
        newProj.Initialize(_parentShooter, _secondaryStat.projectileSpeed.Value, _secondaryStat.damage.Value);
    }
}
