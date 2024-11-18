using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLife : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _turretHealth;
    
    public void TakeDamages(float damageAmount)
    {
        _turretHealth.Value -= damageAmount;
        if (_turretHealth.Value <= 0)
        {
            Destroyed();
        } 
    }

    private void Destroyed()
    {
        Destroy(gameObject);
    }
}
