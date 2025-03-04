using System;
using System.Collections;
using System.Runtime.CompilerServices;
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


    public UnityEvent _onShieldDown, _onShieldUp;

    private Vector3 _respawnPos;
    private bool _isShieldDown, _isInvincible;
    private float _timerRegenShield, _timerInvincibility;
    private float _shieldExplodRange;
    private float _stunCooldownTime;


    private void Start()
    {
        _respawnPos = transform.position;
        _stunCooldownTime = _stunCooldown.Value;
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
    }

    public void ShieldUp()
    {
        _timerRegenShield = 0;
        _isShieldDown = false;

        _onShieldUp.Invoke();
    }

    private void PlayerDeath()
    {
        Rigidbody playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.MovePosition(_respawnPos);

        _timerRegenShield = 0;
        _isShieldDown = false;

        //! quick fix for stuck in QTE after death :)
        GetComponent<QTEControler>()?.KillQTE();
    }

    public void SetRespawnPos(Vector3 position)
    {
        _respawnPos = position;
    }

    public void ExplodeStun()
    {
        if (_stunCooldownTime < _stunCooldown.Value) return;
        _stunCooldownTime = 0;
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
