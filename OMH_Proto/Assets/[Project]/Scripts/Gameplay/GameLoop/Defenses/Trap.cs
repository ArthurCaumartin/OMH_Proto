    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Trap : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField] private FloatReference _trapHitRange;
    [SerializeField] private FloatReference _activationDelay;
    [SerializeField] private FloatReference _damages;
    [SerializeField] private FloatReference _slowStrenght;
    [SerializeField] private FloatReference _slowDuration;
    [SerializeField] private LayerMask _targetLayer;
    [Space]
    [SerializeField] private ParticleSystem _slowParticle;
    [SerializeField] private ParticleSystem _attackParticle;
    private float _activationTime;
    private bool _isActif = false;

    private void Start()
    {
        _attackParticle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isActif)
        {
            _activationTime = _activationDelay.Value;
            return;
        }

        _activationTime += Time.deltaTime;
        if (_activationTime > _activationDelay.Value)
        {
            _isActif = false;
            _activationTime = 0;
            Attack();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        MobLife mob = other.GetComponent<MobLife>();
        if (mob) _isActif = true;
    }

    private void Attack()
    {
        // print("Trap : " + name + " attack !");
        StartCoroutine(AttackFx());

        Collider[] col = Physics.OverlapSphere(transform.position, _trapHitRange.Value, _targetLayer);
        for (int i = 0; i < col.Length; i++)
        {
            MobLife t = col[i].GetComponent<MobLife>();
            t?.TakeDamages(gameObject, _damages.Value, DamageType.Unassigned);

            PhysicsAgent agent = col[i].GetComponent<PhysicsAgent>();
            if (agent)
            {
                agent.SlowAgent(_slowStrenght.Value / 100, _slowDuration.Value);
                if (_slowParticle)
                    Destroy(Instantiate(_slowParticle, agent.transform), _slowDuration.Value);
            }
        }
    }

    IEnumerator AttackFx()
    {
        _attackParticle.Clear();
        _attackParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(_activationDelay.Value * .95f);
        _attackParticle.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(1, 0, 0, .1f);
        Gizmos.DrawSphere(transform.position, _trapHitRange.Value);
    }
}
