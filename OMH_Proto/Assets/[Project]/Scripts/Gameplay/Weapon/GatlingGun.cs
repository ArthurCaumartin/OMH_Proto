using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GatlingGun : Weapon
{
    [Header("Gun Main Fire Modifier :")]
    [SerializeField] private FloatReference _spread;
    [SerializeField] private FloatReference _randomness;
    [Space]
    [SerializeField] private FloatReference _gatlingMunitions;
    [SerializeField] private TextMeshProUGUI _gatlingMunitionsText;
    
    private float _currentGatlingMunition;

    private void Awake()
    {
        _currentGatlingMunition = _gatlingMunitions.Value;
    }

    public void GatlingShot()
    {
        _currentGatlingMunition --;
        _gatlingMunitionsText.text = _currentGatlingMunition.ToString();
        if (_currentGatlingMunition <= 0)
        {
            WeaponControler weaponControler = GetComponentInParent<WeaponControler>();
            weaponControler.RemoveWeapon(this);
            Destroy(gameObject);
        }
    }
    public override void Attack()
    {
        base.Attack();
        
        Projectile newProj = Instantiate(_projectile, transform.position, transform.rotation);
        newProj.Initialize(_parentShooter, _stat.projectileSpeed.Value, _stat.damage.Value);

        float randomAngle = Random.Range(-_spread.Value, _spread.Value);
        float x = Mathf.InverseLerp(-45, 45, randomAngle);
        float angleValue = Mathf.Lerp(-1, 1, x);
        Vector3 newOrientation = new Vector3(angleValue, 0, 1);
        
        newOrientation = newOrientation.normalized;
        newProj.transform.forward = transform.rotation * newOrientation;

        GatlingShot();
    }

    public override void SecondaryAttack()
    {
        if (_secondaryProjectile == null) return;
        
        base.SecondaryAttack();
        Projectile newProj = Instantiate(_secondaryProjectile, transform.position, transform.rotation);
        newProj.Initialize(_parentShooter, _secondaryStat.projectileSpeed.Value, _secondaryStat.damage.Value);
    }
}
