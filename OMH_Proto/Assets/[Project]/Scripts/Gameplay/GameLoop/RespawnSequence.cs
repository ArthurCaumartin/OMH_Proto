using System.Collections;
using UnityEngine;

public class RespawnSequence : MonoBehaviour
{
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private float _gameplayDisableDuration = 5;
    [Space]
    [SerializeField] private GameObject[] _objectToHideArray;
    private Shield _shield;
    private Rigidbody _rigidbody;
    private QTEControler _qteControler;
    private PlayerMovement _movement;
    private PlayerAim _aim;
    private PlayerInteract _interact;
    private WeaponControler _weaponControler;
    private MobTarget _mobTarget;
    private PlayerDisolve _playerDisolve;

    private void Start()
    {
        _shield = GetComponent<Shield>();
        _aim = GetComponent<PlayerAim>();
        _rigidbody = GetComponent<Rigidbody>();
        _mobTarget = GetComponent<MobTarget>();
        _movement = GetComponent<PlayerMovement>();
        _interact = GetComponent<PlayerInteract>();
        _qteControler = GetComponent<QTEControler>();
        _weaponControler = GetComponent<WeaponControler>();

        _playerDisolve = GetComponent<PlayerDisolve>();

        _shield.OnDeathEvent.AddListener(Respawn);
    }
    private void Respawn()
    {
        _qteControler.KillQTE();

        if (ScreenHider.instance)
        {
            StartCoroutine(DisableAllForDuration(_gameplayDisableDuration));
            ScreenHider.instance?.HideScreenForDuration(1, .3f, () =>
            {
                _rigidbody.MovePosition(_respawnPoint.position);
                _rigidbody.velocity = Vector3.zero;
                _playerDisolve.ShowPlayerWithDisolve(_gameplayDisableDuration);
            });
        }
        else
        {
            StartCoroutine(DisableAllForDuration(_gameplayDisableDuration));
            _rigidbody.MovePosition(_respawnPoint.position);
        }
    }

    private void EnableGameplay(bool value)
    {
        _qteControler.enabled = value;
        _movement.enabled = value;
        _aim.enabled = value;
        _interact.enabled = value;
        _weaponControler.enabled = value;
        _mobTarget.enable = value;

        foreach (var item in _objectToHideArray)
            item.SetActive(value);
    }

    private IEnumerator DisableAllForDuration(float duration)
    {
        EnableGameplay(false);
        yield return new WaitForSeconds(duration);
        EnableGameplay(true);
    }
}


