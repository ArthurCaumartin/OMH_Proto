using System;
using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private Material _shieldUpMaterial, _shieldDownMaterial;
    [SerializeField] private Renderer _shieldMeshRenderer;
    [SerializeField] private FloatReference _timeShieldRegen;
    [SerializeField] private FloatReference _playerInvincibiltyDuration;
    [SerializeField] private FloatVariable _playerMovementSpeed;
    [SerializeField] private FloatReference _shieldBoostMoveSpeed;

    [SerializeField] private AnimatorBoolSetter _shieldAnim;

    private Vector3 _respawnPos;


    private bool _isShieldDown, _isInvincible;
    private float _timerRegenShield, _timerInvincibility;

    public void Awake()
    {
        _respawnPos = transform.position;
    }

    private void Update()
    {
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

    public void TakeDamages(float damageAmount)
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

    private void ShieldDown()
    {
        _isShieldDown = true;
        _isInvincible = true;

        _shieldAnim.SetParametre(true);

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldDownMaterial;
        if (_playerMovementSpeed) _playerMovementSpeed.Value = _shieldBoostMoveSpeed.Value;
    }

    private void ShieldUp()
    {
        _timerRegenShield = 0;
        _isShieldDown = false;

        _shieldAnim.SetParametre(false);

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldUpMaterial;
        if (_playerMovementSpeed) _playerMovementSpeed.Value = 1;
    }

    private void PlayerDeath()
    {
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
}
