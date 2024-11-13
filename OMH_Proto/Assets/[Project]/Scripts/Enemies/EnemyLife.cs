using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float _mobHealth;
    [SerializeField] private Material _hitMaterial, _baseMaterial;
    [SerializeField] private MeshRenderer _enemyMesh;

    public void DoDamages(float value)
    {
        print("Hitted");

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
        _enemyMesh.material = _hitMaterial;
        yield return new WaitForSeconds(0.2f);
        _enemyMesh.material = _baseMaterial;
    }
}
