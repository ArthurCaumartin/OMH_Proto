using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// transform.forward set by playerControler with parent rotation
/// </summary>
public class Weapon : MonoBehaviour
{
    [Header("Visual : ")]
    [SerializeField] private Transform _meshPivot;
    [SerializeField] private string _animationStateName = "Pistol_Aim";

    [Header("Main Fire : ")]
    [SerializeField] protected Projectile _projectile;
    [SerializeField] protected StatContainer _stat;

    [Header("Secondary Fire :")]
    [SerializeField] protected Projectile _secondaryProjectile;
    [SerializeField] private FloatReference _secondaryCooldown;
    [SerializeField] private FloatReference _secondaryDynamicCoolDown;
    [SerializeField] protected StatContainer _secondaryStat;
    private float _attackTime;


    private WeaponVisual _weaponVisual;
    protected GameObject _parentShooter;
    private WeaponControler _weaponControler;
    private int _animationStateHash;

    public Transform MeshTransform { get => _meshPivot; }
    public int AnimationState { get => _animationStateHash; }

    private void Start()
    {
        _parentShooter = GetComponentInParent<PlayerAim>().gameObject;
        _weaponVisual = GetComponent<WeaponVisual>();
        _secondaryDynamicCoolDown.Value = _secondaryCooldown.Value;
    }

    private void OnValidate()
    {
        _animationStateHash = Animator.StringToHash(_animationStateName);
    }

    private void OnEnable()
    {
        _attackTime = 0;
    }

    public void Initialize(WeaponControler controler)
    {
        _weaponControler = controler;
    }

    public virtual void Attack()
    {
        _weaponVisual.PlayVisual(Vector3.one * .5f);
    }

    public virtual void SecondaryAttack()
    {
        _weaponVisual.PlayVisual(Vector3.one * .8f);
    }

    private void Update()
    {
        _attackTime += Time.deltaTime;
        _secondaryDynamicCoolDown.Value += Time.deltaTime;

        if (_attackTime > 1 / _stat.attackPerSecond.Value)
        {
            if (_weaponControler.IsPrimaryAttacking)
            {
                _attackTime = 0;
                Attack();
            }
        }

        if (_weaponControler.IsSecondaryAttacking && _secondaryDynamicCoolDown.Value > _secondaryCooldown.Value)
        {
            _secondaryDynamicCoolDown.Value = 0;
            SecondaryAttack();
        }
    }

}
