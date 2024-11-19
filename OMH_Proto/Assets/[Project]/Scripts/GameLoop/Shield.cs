using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private Material _shieldUpMaterial, _shieldDownMaterial;
    [SerializeField] private Renderer _shieldMeshRenderer;
    [SerializeField] private FloatReference _timeShieldRegen;
    [SerializeField] private FloatReference _playerMovementSpeed;
    [SerializeField] private FloatReference _playerBoostMoveSpeed;

    [SerializeField] private AnimatorTriggerSetter _shieldAnimator;

    private bool _isShieldDown;
    private float _timer;

    private void Update()
    {
        if (_isShieldDown)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeShieldRegen.Value)
            {
                ShieldUp();
            }
        }
    }

    public void TakeDamages(float damageAmount)
    {
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
        
        _shieldAnimator.SetParametre();

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldDownMaterial;
        _playerMovementSpeed.Value += _playerBoostMoveSpeed.Value;
    }

    private void ShieldUp()
    {
        _timer = 0;
        _isShieldDown = false;

        if (_shieldMeshRenderer) _shieldMeshRenderer.material = _shieldUpMaterial;
        _playerMovementSpeed.Value -= _playerBoostMoveSpeed.Value;
    }

    private void PlayerDeath()
    {
        Destroy(gameObject);
    }
}
