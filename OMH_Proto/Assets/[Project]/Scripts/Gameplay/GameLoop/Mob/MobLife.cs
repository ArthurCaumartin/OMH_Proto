using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobLife : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _mobMaxHealth;
    [SerializeField] private Renderer _renderer;
    private float _currentHealth;
    [SerializeField] private UnityEvent<MobLife, DamageType> _onDeathEvent;
    [SerializeField] private UnityEvent<GameObject, DamageType> _onDamageTakenEvent;
    public UnityEvent<GameObject, DamageType> OnDamageTakenEvent { get => _onDamageTakenEvent; }
    public UnityEvent<MobLife, DamageType> OnDeathEvent { get => _onDeathEvent; }
    private HealthBar _healthBar;

    private void Start()
    {
        _currentHealth = _mobMaxHealth.Value;
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar?.SetFillAmount(1);
    }

    public void TakeDamages(GameObject damageDealer, float value, DamageType type)
    {
        if (value > 0)
        {
            if (_renderer) StartCoroutine(Hit());
            _onDamageTakenEvent.Invoke(damageDealer, type);
        }

        _currentHealth -= value;
        _healthBar.SetFillAmount(Mathf.InverseLerp(0, _mobMaxHealth.Value, _currentHealth), true);
        if (_currentHealth <= 0)
        {
            _onDeathEvent.Invoke(this, type);
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public IEnumerator Hit()
    {
        _renderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.2f);
        _renderer.material.SetColor("_Color", Color.white);
    }
}
