using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private Material _shieldUpMaterial, _shieldDownMaterial;
    [SerializeField] private Renderer _shieldMeshRenderer;
    [SerializeField] private FloatReference _timeShieldRegen;
    [SerializeField] private FloatReference _playerMovementSpeed;
    [SerializeField] private FloatReference _playerBoostMoveSpeed;
    [SerializeField] private FloatReference _playerInvincibiltyDuration;

    [SerializeField] private AnimatorTriggerSetter _shieldDownAnim, _shieldUpAnim;

    private bool _isShieldDown, _isInvincible;
    private float _timerRegenShield, _timerInvincibility;

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
        
        _shieldDownAnim.SetParametre();

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldDownMaterial;
        _playerMovementSpeed.Value += _playerBoostMoveSpeed.Value;
    }

    private void ShieldUp()
    {
        _timerRegenShield = 0;
        _isShieldDown = false;
        
        _shieldUpAnim.SetParametre();

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldUpMaterial;
        _playerMovementSpeed.Value -= _playerBoostMoveSpeed.Value;
    }

    private void PlayerDeath()
    {
        Destroy(gameObject);
    }
}
