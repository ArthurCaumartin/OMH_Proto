using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField] private FloatReference _trapHitRange;
    [SerializeField] private FloatReference _activationDelay;
    [SerializeField] private FloatReference _damages;
    [SerializeField] private FloatReference _slowStrenght;
    [SerializeField] private FloatReference _slowDuration;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private List<GameObject> _visualList;
    private float _activationTime;
    private bool _isActif = false;
    private Renderer[] _rendererArray;
    private Color _baseColorBackup;

    private void Start()
    {
        EnableVisual(false);

        _rendererArray = GetComponentsInChildren<Renderer>();
        _baseColorBackup = _rendererArray[0].materials[1].GetColor("_EmissionColor");
    }

    private void Update()
    {
        if (!_isActif)
        {
            _activationTime = 0;
            LerpEmissive(0);
            return;
        }

        _activationTime += Time.deltaTime;
        LerpEmissive(_activationTime / _activationDelay.Value);
        if (_activationTime > _activationDelay.Value)
        {
            _isActif = false;
            _activationTime = 0;
            Activate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        MobLife mob = other.GetComponent<MobLife>();
        if (mob) _isActif = true;
    }

    private void Activate()
    {
        StartCoroutine(VisualTrap());

        Collider[] col = Physics.OverlapSphere(transform.position, _trapHitRange.Value, _targetLayer);
        for (int i = 0; i < col.Length; i++)
        {
            MobLife t = col[i].GetComponent<MobLife>();
            t?.TakeDamages(gameObject, _damages.Value, DamageType.Unassigned);

            PhysicsAgent y = col[i].GetComponent<PhysicsAgent>();
            y?.SlowAgent(_slowStrenght.Value / 100, _slowDuration.Value);
        }
    }

    IEnumerator VisualTrap()
    {
        EnableVisual(true);
        yield return new WaitForSeconds(0.3f);
        EnableVisual(false);
    }

    private void EnableVisual(bool value)
    {
        foreach (var item in _visualList)
            item.SetActive(value);
    }

    private void LerpEmissive(float time)
    {
        print("l time : " + time);
        foreach (var item in _rendererArray)
        {
            try
            {
                item.materials[1].SetColor("_EmissionColor", Color.Lerp(_baseColorBackup, Color.red, time));
            }
            catch { }
        }
    }

    private void OnDrawGizmos()
    {
        if(!DEBUG) return;
        Gizmos.color = new Color(1, 0, 0, .1f);
        Gizmos.DrawSphere(transform.position, _trapHitRange.Value);
    }
}
