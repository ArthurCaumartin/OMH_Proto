using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO set la durée du destroy avec la range et la speed
    //TODO changer TazerEffect ref par un truc plus generique pour avoir plusieur effet possible ?
    [SerializeField] private float _pushForce = 5;
    [SerializeField] private GameObject _shootEffect;
    [SerializeField] private FloatReference _effectPropagationRange;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private LayerMask _effectLayer;
    private float _speed;
    private float _damage;
    private GameObject _shooter;
    private Rigidbody _rb;
    private Vector3 _lastFramePosition;
    [SerializeField] private AK.Wwise.Event _shootSound;
    [SerializeField] private AK.Wwise.RTPC _RTPCWeapon;
    
    [ContextMenu("Test RTPC Set")]
    private void TestRTPCSet()
    {
        if (_shooter == null)
        {
            Debug.LogWarning("Shooter not set.");
            return;
        }

        var weaponId = _shooter.GetComponent<WeaponIdentifier>();
        if (weaponId == null)
        {
            Debug.LogWarning("WeaponIdentifier not found.");
            return;
        }

        float value = 0;
        switch (weaponId.weaponType)
        {
            case WeaponType.Fugitive: value = 0; break;
            case WeaponType.Sobek: value = 1; break;
            case WeaponType.Gatling: value = 2; break;
        }

        _RTPCWeapon.SetValue(gameObject, value);
        Debug.Log($"[Test] RTPC set to {value} for {weaponId.weaponType}");
    }
    public Projectile Initialize(GameObject shooter, float speed, float damage, WeaponIdentifier weaponID = null)
    {
        print(name + " Initialize");

        _shooter = shooter;
        _speed = speed;
        _damage = damage;
        

        _rb = GetComponent<Rigidbody>();
        _lastFramePosition = _rb.position;
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);

        PlayShootSwitch(weaponID);
        _shootSound.Post(gameObject);

        Destroy(gameObject, 1f);
        return this;
    }

    // faut encore fix les proj
    // en raycast y depase les mob
    // en collider c chaud de filtrer les layer

    private void FixedUpdate()
    {
        Physics.Linecast(transform.position, _lastFramePosition, out RaycastHit hit, _wallLayer);
        if (hit.collider)
            Destroy(gameObject);

        _lastFramePosition = _rb.position;
    }

    private void PlayShootSwitch(WeaponIdentifier weaponId)
    {
        // if (_shooter == null)
        // {
        //     Debug.LogWarning("Shooter is null");
        //     return;
        // }

        // var weaponId = _shooter.GetComponent<WeaponIdentifier>();
        if (weaponId == null)
        {
            Debug.LogWarning("WeaponIdentifier not found on shooter or its children.");
            return;
        }

        print("IS name : " + weaponId.name);

        float value = -1;
        switch (weaponId.weaponType)
        {
            case WeaponType.Fugitive:
                AudioDebugLog.LogAudio(this.GetType().ToString(), ToString(), "Bullet from fugitive");
                value = 0;
                break;
            case WeaponType.Sobek:
                AudioDebugLog.LogAudio(this.GetType().ToString(), ToString(), "Bullet from sobek");
                value = 1;
                break;
            case WeaponType.Gatling:
                AudioDebugLog.LogAudio(this.GetType().ToString(), ToString(), "Bullet from gatling");
                value = 2;
                break;
            default:
                Debug.LogWarning("Unknown WeaponType: " + weaponId.weaponType);
                break;
        }
        _RTPCWeapon.SetGlobalValue(value);
        AudioDebugLog.LogAudio(this.GetType().ToString(), ToString(), $"RTPC Weapon set to {value} for {weaponId.weaponType}");
    }

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
        if (damagable != null && other.tag != "Defenses")
        {
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            if (_pushForce != 0) otherRb.AddForce(transform.forward * _pushForce, ForceMode.Impulse);

            damagable.TakeDamages(_shooter, _damage, DamageType.Unassigned);
            if (_shootEffect) AddShootEffect();

            Destroy(gameObject);
        }
    }
}
