using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traptest : MonoBehaviour
{
    [SerializeField] private FloatReference _trapHitRange, _trapActivationTimer, _trapDamages, _trapSlowStrenght, _trapSlowDuration;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private GameObject _visualTrap;
    [SerializeField] private GameObject _TrapVFX;
    
    private float _timer;

    private void Start()
    {
        _timer = _trapActivationTimer.Value;
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        MobLife mob = other.GetComponent<MobLife>();
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
            MobLife t = col[i].GetComponent<MobLife>();
            if (t)
            {
                t.TakeDamages(_trapDamages.Value);
            }
            PhysicsAgent y = col[i].GetComponent<PhysicsAgent>();
            if (y)
            {
                y.SlowAgent(_trapSlowStrenght.Value / 100, _trapSlowDuration.Value);
            }
        }
    }

    IEnumerator VisualTrap()
    {
        _visualTrap.SetActive(true);
        _TrapVFX.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _visualTrap.SetActive(false);
        _TrapVFX.SetActive(false);
    }
}
