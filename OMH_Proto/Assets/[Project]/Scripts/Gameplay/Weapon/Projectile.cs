using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO set la durée du destroy avec la range et la speed
    //TODO changer TazerEffect ref par un truc plus generique pour avoir plusieur effet possible ?
    [SerializeField] private GameObject _shootEffect;
    [SerializeField] private FloatReference _effectPropagationRange;
    [SerializeField] private LayerMask _effectLayer;
    [SerializeField] private LayerMask _projectileLayer;
    private float _speed;
    private float _damage;
    private GameObject _shooter;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 1f);
    }

    public Projectile Initialize(GameObject shooter, float speed, float damage)
    {
        _shooter = shooter;
        _speed = speed;
        _damage = damage;
        return this;
    }

    private void FixedUpdate()
    {
        Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, _speed * Time.fixedDeltaTime, _projectileLayer);
        Debug.DrawRay(transform.position, -transform.forward * _speed * Time.fixedDeltaTime, Color.red);
        if (hit.collider)
        {
            print("Hit " + hit.collider.gameObject.name);

            MobLife enemyLife = hit.collider.gameObject.GetComponent<MobLife>();
            if (enemyLife)
            {
                enemyLife?.TakeDamages(_shooter, _damage, DamageType.Unassigned);
                if (_shootEffect) AddShootEffect();
            }

            Destroy(gameObject);
        }
        _rb.MovePosition(_rb.position + (transform.forward * Time.fixedDeltaTime * _speed));
    }


    public void AddShootEffect()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, _effectPropagationRange.Value, _effectLayer);
        if (colls.Length == 0) return;
        for (int i = 0; i < colls.Length; i++)
            Instantiate(_shootEffect, colls[i].transform).GetComponent<IEffectable>().InitializeEffect(_effectPropagationRange.Value, transform.position);
    }
}
