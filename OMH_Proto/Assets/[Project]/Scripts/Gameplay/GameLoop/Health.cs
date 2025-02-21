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
    private HealthBar _healthBar;

    private void Start()
    {
        _currentHealth.Value = _maxHealth.Value;
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar?.SetFillAmount(1, false);
    }

    public void TakeDamages(GameObject damageDealer, float damageAmount, DamageType type = DamageType.Unassigned)
    {
        _onDamageTaken.Invoke(damageAmount);
        _currentHealth.Value -= damageAmount;
        _healthBar?.SetFillAmount(Mathf.InverseLerp(0, _maxHealth.Value, _currentHealth.Value));
        if (_currentHealth.Value <= 0) Destroy(gameObject);
    }
}
