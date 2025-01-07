using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLife : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _turretHealth;
    [SerializeField] private GameObject _objectToDestroy;
    private float _health;

    private void Start()
    {
        _health = _turretHealth.Value;
    }

    public void TakeDamages(float damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            Destroyed();
        } 
    }

    private void Destroyed()
    {
        if(_objectToDestroy) Destroy(_objectToDestroy);
        else
        {
            Destroy(gameObject);
        }
    }
}
