using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobLife : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _mobMaxHealth;
    [SerializeField] private GameEvent _onDeathGameEvent;
    [SerializeField] private MobAnimationControler _animationControler;
    [SerializeField] private Transform _mobRendererTransform;
    private float _currentHealth;
    [SerializeField] private UnityEvent<MobLife, DamageType> _onDeathEvent;
    [SerializeField] private UnityEvent<GameObject, DamageType> _onDamageTakenEvent;
    public UnityEvent<GameObject, DamageType> OnDamageTakenEvent { get => _onDamageTakenEvent; }
    public UnityEvent<MobLife, DamageType> OnDeathEvent { get => _onDeathEvent; }
    private HealthBar _healthBar;
    private DisolveEffect _disolveEffect;

    private void Start()
    {
        _currentHealth = _mobMaxHealth.Value;
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar?.SetFillAmount(1);
        _animationControler = GetComponentInChildren<MobAnimationControler>();
        _disolveEffect = GetComponentInChildren<DisolveEffect>();
    }

    public void TakeDamages(GameObject damageDealer, float value, DamageType type)
    {
        if (value > 0)
        {
            _onDamageTakenEvent.Invoke(damageDealer, type);
        }

        _currentHealth -= value;
        _healthBar.SetFillAmount(Mathf.InverseLerp(0, _mobMaxHealth.Value, _currentHealth));
        if (_currentHealth <= 0)
        {
            _onDeathEvent.Invoke(this, type);
            Death();
        }
    }

    private void Death()
    {
        _animationControler.PlayDeathAnimation();
        _mobRendererTransform.parent = null;
        _onDeathGameEvent.Raise();
        _disolveEffect?.Disolve(true, true);
        Destroy(gameObject);
    }
}
