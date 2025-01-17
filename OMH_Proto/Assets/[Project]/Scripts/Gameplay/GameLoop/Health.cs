using System;
using UnityEngine;


public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _maxHealth;
    [SerializeField] private FloatReference _currentHealth;

    private void Start()
    {
        _currentHealth.Value = _maxHealth.Value;
    }

    public void TakeDamages(float damageAmount)
    {
        _currentHealth.Value -= damageAmount;
        if (_currentHealth.Value <= 0) Destroy(gameObject);
    }
}
