using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO add range for proj life time
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
        Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, .2f, _mobLayer);
        if(hit.collider)
        {
            //TODO do damage :)
            Destroy(gameObject);
        }
    }
}
