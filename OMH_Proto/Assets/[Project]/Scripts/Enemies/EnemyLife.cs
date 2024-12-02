using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyLife : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _mobHealth;
    private float _health;
    // [SerializeField] private Material _hitMaterial, _baseMaterial;
    // [SerializeField] private Renderer _enemyRenderer;

    [SerializeField] private UnityEvent<EnemyLife> _onDeathEvent;
    [SerializeField] private UnityEvent _onDamageEvent;
    public UnityEvent OnDamageEvent { get => _onDamageEvent; }
    public UnityEvent<EnemyLife> OnDeathEvent { get => _onDeathEvent; }

    private void Start()
    {
        _health = _mobHealth.Value;

        // if (_enemyRenderer) _enemyRenderer.material = _baseMaterial;
    }

    public void TakeDamages(float value)
    {
        // print("Hitted");
        if (value > 0)
        {
            // StartCoroutine(Hit());
            _onDamageEvent.Invoke();
        }
        
        _health -= value;
        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    // public IEnumerator Hit()
    // {
    //     if (!_enemyRenderer) yield return null;
    //     _enemyRenderer.material = _hitMaterial;
    //     yield return new WaitForSeconds(0.2f);
    //     _enemyRenderer.material = _baseMaterial;
    // }
}
