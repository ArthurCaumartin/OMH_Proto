using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO set la durée du destroy avec la range et la speed
    //TODO changer TazerEffect ref par un truc plus generique pour avoir plusieur effet possible ?
    [SerializeField] private GameObject _shootEffect;
    [SerializeField] private FloatReference _shootRange;
    [SerializeField] private LayerMask _effectLayer;
    [SerializeField] private LayerMask _layer;
    private GameObject _shooter;
    private StatContainer _stat;
    private LineRenderer _line;

    private void Start()
    {
        _line = GetComponentInChildren<LineRenderer>();
    }

    public Projectile Initialize(GameObject shooter, StatContainer stats)
    {
        _stat = stats;
        return this;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _stat.projectileSpeed.Value * Time.deltaTime);
        Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, 1f, _layer);
        if (hit.collider)
        {
            MobLife enemyLife = hit.collider.gameObject.GetComponent<MobLife>();
            DealDamage(enemyLife);
        }
    }

    private void DealDamage(MobLife enemyLife)
    {
        if (!enemyLife) return;
        enemyLife?.TakeDamages(_shooter, _stat.damage.Value, DamageType.Unassigned);
        if (_shootEffect) AddShootEffect();
    }

    public void AddShootEffect()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, _shootRange.Value, _effectLayer);
        if (colls.Length == 0) return;
        for (int i = 0; i < colls.Length; i++)
            Instantiate(_shootEffect, colls[i].transform).GetComponent<IEffectable>().InitializeEffect(_shootRange.Value, transform.position);
    }
}
