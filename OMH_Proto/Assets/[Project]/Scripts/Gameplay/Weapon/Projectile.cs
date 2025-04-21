using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO set la durée du destroy avec la range et la speed
    //TODO changer TazerEffect ref par un truc plus generique pour avoir plusieur effet possible ?
    [SerializeField] private float _pushForce = 5;
    [SerializeField] private GameObject _shootEffect;
    [SerializeField] private FloatReference _effectPropagationRange;
    [SerializeField] private LayerMask _effectLayer;
    [SerializeField] private LayerMask _projectileLayer;
    [SerializeField] public AK.Wwise.Event _shootSound;
    private float _speed;
    private float _damage;
    private GameObject _shooter;
    private Rigidbody _rb;
    private Vector3 _lastFramePosition;
    

    public Projectile Initialize(GameObject shooter, float speed, float damage)
    {
        Debug.Log("<color=orange>From script [Projectile.cs] : Pan !</color>");
        _shooter = shooter;
        _speed = speed;
        _damage = damage;

        _rb = GetComponent<Rigidbody>();
        _lastFramePosition = _rb.position;
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        _shootSound.Post(gameObject);

        Destroy(gameObject, 1f);
        return this;
    }

    // private void FixedUpdate()
    // {
    //     Physics.Linecast(transform.position, _lastFramePosition, out RaycastHit hit, _projectileLayer);
    //     if (hit.collider)
    //     {
    //         // print("Hit " + hit.collider.gameObject.name);
    //         IDamageable damagable = hit.collider.gameObject.GetComponent<MobLife>();
    //         if (damagable != null)
    //         {
    //             damagable?.TakeDamages(_shooter, _damage, DamageType.Unassigned);
    //             if (_shootEffect) AddShootEffect();
    //         }

    //         Destroy(gameObject);
    //     }

    //     _lastFramePosition = _rb.position;
    // }


    public void AddShootEffect()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, _effectPropagationRange.Value, _effectLayer);
        if (colls.Length == 0) return;
        for (int i = 0; i < colls.Length; i++)
            Instantiate(_shootEffect, colls[i].transform).GetComponent<IEffectable>().InitializeEffect(_effectPropagationRange.Value, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damagable = other.gameObject.GetComponent<MobLife>();
        if (damagable != null)
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            if (_pushForce != 0) otherRb.AddForce(transform.forward * _pushForce, ForceMode.Impulse);

            damagable.TakeDamages(_shooter, _damage, DamageType.Unassigned);
            if (_shootEffect) AddShootEffect();

            Destroy(gameObject);
        }
    }
}
