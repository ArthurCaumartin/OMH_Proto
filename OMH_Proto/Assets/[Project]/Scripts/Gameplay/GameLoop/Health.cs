using System;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _maxHealth;
    [SerializeField] private FloatReference _currentHealth;
    [Space]
    [Tooltip("Call when damage is taken, send damage amount as parameter.")]
    [SerializeField] private UnityEvent<float> _onDamageTaken;

    private void Start()
    {
        _currentHealth.Value = _maxHealth.Value;
    }

    public void TakeDamages(GameObject damageDealer, float damageAmount, DamageType type = DamageType.Unassigned)
    {
        _onDamageTaken.Invoke(damageAmount);
        _currentHealth.Value -= damageAmount;
        if (_currentHealth.Value <= 0) Destroy(gameObject);
    }
}
