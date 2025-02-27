using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CanEditMultipleObjects]
public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _maxHealth;
    [SerializeField] private FloatReference _currentHealth;
    [Space]
    [Tooltip("Call when damage is taken, send damage amount as parameter.")]
    [SerializeField] private UnityEvent<float> _onDamageTaken;
    private HealthBar _healthBar;

    public UnityEvent<float> OnDamageTaken { get => _onDamageTaken; }

    private void Start()
    {
        _currentHealth.Value = _maxHealth.Value;
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar?.SetFillAmount(GetHealtRatio(), _currentHealth.Value != _maxHealth.Value);
    }

    public void TakeDamages(GameObject damageDealer, float damageAmount, DamageType type = DamageType.Unassigned)
    {
        _onDamageTaken.Invoke(damageAmount);
        _currentHealth.Value -= damageAmount;
        _healthBar?.SetFillAmount(GetHealtRatio());
        if (_currentHealth.Value <= 0) Destroy(gameObject);
    }

    public void Heal(GameObject healer, float healAmount)
    {
        _currentHealth.Value += healAmount;
        _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, -1000, _maxHealth.Value);
        _healthBar?.SetFillAmount(GetHealtRatio());
    }

    public float GetHealtRatio()
    {
        return Mathf.InverseLerp(0, _maxHealth.Value, _currentHealth.Value);
    }
}
