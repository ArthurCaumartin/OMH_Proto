using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Weapon : MonoBehaviour
{
    //! /////////////////////////////////////////////////////////////
    //? transform.forward set by playerControler with parent rotation
    //! /////////////////////////////////////////////////////////////
    [SerializeField] protected Projectile _projectile;
    [SerializeField] protected StatContainer _stat;
    private float _attackTime;

    public virtual void Attack()
    {
        print("Piou piou");
    }

    private void Update()
    {
        _attackTime += Time.deltaTime;
    }

    private void OnAttack(InputValue value)
    {
        if (value.Get<float>() > .5f && _attackTime > 1 / _stat.attackPerSecond.Value)
        {
            _attackTime = 0;
            Attack();
        }
    }
}
