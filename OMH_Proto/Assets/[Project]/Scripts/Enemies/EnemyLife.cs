using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyLife : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _mobHealth;
    [SerializeField] private Renderer _renderer;
    private float _health;
    [SerializeField] private UnityEvent<EnemyLife> _onDeathEvent;
    [SerializeField] private UnityEvent _onDamageEvent;
    public UnityEvent OnDamageEvent { get => _onDamageEvent; }
    public UnityEvent<EnemyLife> OnDeathEvent { get => _onDeathEvent; }

    private void Start()
    {
        _health = _mobHealth.Value;
    }

    public void TakeDamages(float value)
    {
        // print("Hitted");
        if (value > 0)
        {
            if(_renderer)StartCoroutine(Hit());
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

    public IEnumerator Hit()
    {
        _renderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.2f);
        _renderer.material.SetColor("_Color", Color.white);
    }
}
