using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLife : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _turretHealth;
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
        Destroy(gameObject);
    }
}
