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

    [Header("Gatling Visual :")]
    [SerializeField] private Rotate _cannonRotate;
    [SerializeField] private float _passiveRotateSpeed = 2;
    [SerializeField] private float _activeRotateSpeed = 10;

    private float _currentGatlingMunition;

    private void Awake()
    {
        _currentGatlingMunition = _gatlingMunitions.Value;
    }

    public override void Update()
    {
        base.Update();
        _cannonRotate.Speed = _weaponControler.IsPrimaryAttacking ? _activeRotateSpeed : _passiveRotateSpeed;
    }

    public void GatlingShot()
    {
        _currentGatlingMunition--;
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

        Projectile newProj = Instantiate(_projectile, _shootPoint.position, transform.rotation);
        newProj.Initialize(_parentShooter, _stat.projectileSpeed.Value, _stat.damage.Value, _weaponID, false);

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
        newProj.Initialize(_parentShooter, _secondaryStat.projectileSpeed.Value, _secondaryStat.damage.Value, _weaponID);
    }
}
