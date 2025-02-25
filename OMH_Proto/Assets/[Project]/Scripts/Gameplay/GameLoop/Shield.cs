using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Shield : Upgradable, IDamageable
{
    public bool DEBUG = false;
    [Header("Shield Stat")]
    [SerializeField] private FloatReference _timeShieldRegen;
    [SerializeField] private FloatReference _playerInvincibiltyDuration;
    [SerializeField] private FloatVariable _playerMovementSpeed;
    [SerializeField] private FloatReference _shieldBoostMoveSpeed;
    [Space]
    [SerializeField] private FloatReference _stunDuration;
    [SerializeField] private FloatReference _stunCooldown;
    [SerializeField] private UpgradeMeta _shildExplodeUpgrade;
    [SerializeField] private LayerMask _mobLayer;

    [Header("Visual")]
    [SerializeField] private Material _shieldDownMaterial;
    [SerializeField] private Material _shieldUpMaterial;
    [SerializeField] private Renderer _shieldMeshRenderer;
    [SerializeField] private AnimatorBoolSetter _shieldAnim;
    [Space]
    public UnityEvent _onShieldDown, _onShieldUp;

    private Vector3 _respawnPos;
    private bool _isShieldDown, _isInvincible;
    private float _timerRegenShield, _timerInvincibility;
    private float _shieldExplodRange;
    private float _stunCooldownTime;


    private void Start()
    {
        _respawnPos = transform.position;
    }

    private void Update()
    {
        _stunCooldownTime += Time.deltaTime;
        if (_isShieldDown)
        {
            _timerRegenShield += Time.deltaTime;

            if (_timerRegenShield >= _timeShieldRegen.Value)
            {
                ShieldUp();
            }
        }

        if (_isInvincible)
        {
            _timerInvincibility += Time.deltaTime;
            if (_timerInvincibility >= _playerInvincibiltyDuration.Value)
            {
                _isInvincible = false;
                _timerInvincibility = 0;
            }
        }
    }

    public void TakeDamages(GameObject damageDealer, float damageAmount, DamageType type = DamageType.Unassigned)
    {
        if (_isInvincible) return;

        if (_isShieldDown)
        {
            PlayerDeath();
        }
        else
        {
            ShieldDown();
        }
    }

    public void ShieldDown()
    {
        ExplodeStun();

        _isShieldDown = true;
        _isInvincible = true;

        _onShieldDown.Invoke();

        _shieldAnim.SetParametre(true);

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldDownMaterial;
        if (_playerMovementSpeed) _playerMovementSpeed.Value = _shieldBoostMoveSpeed.Value;
    }

    public void ShieldUp()
    {
        _timerRegenShield = 0;
        _isShieldDown = false;

        _onShieldUp.Invoke();

        _shieldAnim.SetParametre(false);

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldUpMaterial;
        if (_playerMovementSpeed) _playerMovementSpeed.Value = 1;
    }

    private void PlayerDeath()
    {
        GetComponent<QTEControler>().KillQTE();

        Rigidbody playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.MovePosition(_respawnPos);

        _timerRegenShield = 0;
        _isShieldDown = false;

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldUpMaterial;
        if (_playerMovementSpeed) _playerMovementSpeed.Value = 1;
    }

    public void SetRespawnPos(Vector3 position)
    {
        _respawnPos = position;
    }

    public void ExplodeStun()
    {
        if (_stunCooldownTime < _stunCooldown.Value) return;
        _stunCooldownTime = 0;
        print("ExplodeStun");
        Collider[] cols = Physics.OverlapSphere(transform.position, _shieldExplodRange, _mobLayer);
        if (cols.Length == 0) return;
        for (int i = 0; i < cols.Length; i++)
        {
            StateMachine_MobBase mob = cols[i].GetComponent<StateMachine_MobBase>();
            mob?.StunMob(_stunDuration.Value);
        }
    }

    public override void UpdateUpgrade()
    {
        base.UpdateUpgrade();
        _shieldExplodRange = _shildExplodeUpgrade.GetUpgradeValue();
    }

    public void OnDrawGizmos()
    {
        if (!DEBUG) return;
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _shieldExplodRange);
    }
}
