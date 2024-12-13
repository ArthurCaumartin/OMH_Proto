using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO set la durée du destroy avec la range et la speed
    //TODO changer TazerEffect ref par un truc plus generique pour avoir plusieur effet possible ?
    [SerializeField] private GameObject _tasEffect;
    [SerializeField] private FloatReference _taserRange;
    [SerializeField] private LayerMask _effectLayer;
    [SerializeField] private LayerMask _projectileLayer;
    private float _speed;
    private float _damage;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    public Projectile Initialize(float speed, float damage)
    {
        _speed = speed;
        _damage = damage;
        return this;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, 1f, _projectileLayer);
        if (hit.collider)
        {
            // print("Hit " + hit.collider.gameObject.name);

            EnemyLife enemyLife = hit.collider.gameObject.GetComponent<EnemyLife>();
            if (enemyLife)
            {
                enemyLife.TakeDamages(_damage);
                if (_tasEffect) AddTaserEffect();
            }

            Destroy(gameObject);
        }
    }


    public void AddTaserEffect()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, _taserRange.Value, _effectLayer);
        if (colls.Length == 0) return;
        for (int i = 0; i < colls.Length; i++)
            Instantiate(_tasEffect, colls[i].transform).GetComponent<TaserEffect>().Initialize(_taserRange.Value, transform.position);
    }
}
