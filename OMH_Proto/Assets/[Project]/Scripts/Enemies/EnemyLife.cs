using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyLife : MonoBehaviour, IDamageable
{
    [SerializeField] private float _mobHealth;
    [SerializeField] private Material _hitMaterial, _baseMaterial;
    [SerializeField] private Renderer _enemyRenderer;
    
    [SerializeField] private UnityEvent<EnemyLife> _onDeathEvent;
    public UnityEvent<EnemyLife> OnDeathEvent { get => _onDeathEvent; }

    private void Start()
    {
        if (_enemyRenderer) _enemyRenderer.material = _baseMaterial;
    }

    public void TakeDamages(float value)
    {
        // print("Hitted");
        StartCoroutine(Hit());
        _mobHealth -= value;
        if (_mobHealth <= 0)
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
        if(!_enemyRenderer) yield return null;
        _enemyRenderer.material = _hitMaterial;
        yield return new WaitForSeconds(0.2f);
        _enemyRenderer.material = _baseMaterial;
    }
}
