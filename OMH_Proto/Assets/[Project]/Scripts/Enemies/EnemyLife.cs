using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float _mobHealth;
    [SerializeField] private Material _hitMaterial, _baseMaterial;
    [SerializeField] private Renderer _enemyRenderer;

    private void Start()
    {
        _enemyRenderer.material = _baseMaterial;
    }

    public void DoDamages(float value)
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
        _enemyRenderer.material = _hitMaterial;
        yield return new WaitForSeconds(0.2f);
        _enemyRenderer.material = _baseMaterial;
    }
}
