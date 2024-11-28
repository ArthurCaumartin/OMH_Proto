using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private FloatReference _trapHitRange, _trapActivationTimer, _trapDamages, _trapSlowStrenght, _trapSlowDuration;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private GameObject _visualTrap;
    
    private float _timer;
    
    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        EnemyLife mob = other.GetComponent<EnemyLife>();
        if (mob)
        {
            // print("EnemyInRange");
            if (_timer >= _trapActivationTimer.Value)
            {
                _timer = 0;
                Activate();
                print("ActivateTrap");
            }
        }
    }

    private void Activate()
    {
        StartCoroutine(VisualTrap());
        
        Collider[] col = Physics.OverlapSphere(transform.position, _trapHitRange.Value, _targetLayer);
        for (int i = 0; i < col.Length; i++)
        {
            EnemyLife t = col[i].GetComponent<EnemyLife>();
            if (t)
            {
                t.TakeDamages(_trapDamages.Value);
            }
            PhysicsAgent y = col[i].GetComponent<PhysicsAgent>();
            if (y)
            {
                y.SlowAgent(_trapSlowStrenght.Value, _trapSlowDuration.Value);
            }
        }
    }

    IEnumerator VisualTrap()
    {
        _visualTrap.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _visualTrap.SetActive(false);
    }
}
