using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO set la durée du destroy avec la range et la speed
    //TODO changer TazerEffect ref par un truc plus generique pour avoir plusieur effet possible ?
    [SerializeField] private GameObject _shootEffect;
    [SerializeField] private FloatReference _shootRange;
    [SerializeField] private LayerMask _effectLayer;
    [SerializeField] private LayerMask _projectileLayer;
    private float _speed;
    private float _damage;
    private GameObject _shooter;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    public Projectile Initialize(GameObject shooter, float speed, float damage)
    {
        _shooter = shooter;
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

            MobLife enemyLife = hit.collider.gameObject.GetComponent<MobLife>();
            if (enemyLife)
            {
                enemyLife.TakeDamages(_shooter, _damage);
                if (_shootEffect) AddShootEffect();
            }

            Destroy(gameObject);
        }
    }


    public void AddShootEffect()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, _shootRange.Value, _effectLayer);
        if (colls.Length == 0) return;
        for (int i = 0; i < colls.Length; i++)
            Instantiate(_shootEffect, colls[i].transform).GetComponent<IEffectable>().InitializeEffect(_shootRange.Value, transform.position);
    }
}
