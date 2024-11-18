using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO set la durée du destroy avec la range et la speed
    [SerializeField] private LayerMask _mobLayer;
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
        Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, 1f, _mobLayer);
        if(hit.collider)
        {
            print("Hit " + hit.collider.gameObject.name);

            EnemyLife enemyLife = hit.collider.gameObject.GetComponent<EnemyLife>();
            enemyLife.TakeDamages(10);
            
            Destroy(gameObject);
        }
    }
}
