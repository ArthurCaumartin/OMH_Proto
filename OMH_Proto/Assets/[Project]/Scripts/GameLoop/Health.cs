using System;
using UnityEngine;


public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private FloatReference _value;

    public void TakeDamages(float damageAmount)
    {
        _value.Value -= damageAmount;
        if (_value.Value <= 0) Destroy(gameObject);
    }
}
