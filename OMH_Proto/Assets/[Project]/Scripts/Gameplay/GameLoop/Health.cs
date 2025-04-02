using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _maxHealth;
    [SerializeField] private FloatReference _currentHealth;
    [Space]
    [Tooltip("Call when damage is taken, send damage amount as parameter.")]
    [SerializeField] private UnityEvent<float> _onDamageTaken;
    [SerializeField] private UnityEvent _onDeathEvent;
    [SerializeField] private UnityEvent _onHealEvent;
    private HealthBar _healthBar;

    public UnityEvent<float> OnDamageTaken { get => _onDamageTaken; }
    public UnityEvent OnDeathEvent { get => _onDeathEvent; }
    public UnityEvent OnHealEvent { get => _onHealEvent; }


    public bool IsFullLife { get => _currentHealth.Value >= _maxHealth.Value; }


    private void Start()
    {
        _currentHealth.Value = _maxHealth.Value;
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar?.SetFillAmount(GetHealtRatio());
    }

    public void TakeDamages(GameObject damageDealer, float damageAmount, DamageType type = DamageType.Unassigned)
    {
        if (_currentHealth.Value <= 0) return;

        _onDamageTaken.Invoke(damageAmount);
        _currentHealth.Value -= damageAmount;
        _healthBar?.SetFillAmount(GetHealtRatio());
        if (_currentHealth.Value <= 0)
        {
            _onDeathEvent.Invoke();
            Destroy(gameObject);
        }
    }

    public void Heal(GameObject healer, float healAmount)
    {
        print("heal");
        _currentHealth.Value += healAmount;
        _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, -1000, _maxHealth.Value);
        _healthBar?.SetFillAmount(GetHealtRatio());
        _onHealEvent.Invoke();
    }

    public float GetHealtRatio()
    {
        return Mathf.InverseLerp(0, _maxHealth.Value, _currentHealth.Value);
    }

    private IEnumerator DestoryDelais(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
