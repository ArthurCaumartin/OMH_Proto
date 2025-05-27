using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RespawnSequence : MonoBehaviour
{
    [SerializeField] private RespawnPoint _respawnPoint;
    [SerializeField] private float _gameplayDisableDuration = 5;
    [SerializeField] private Volume _baseGameVolume;
    [SerializeField] private Volume _deathVolume;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private AnimationCurve _canvasAlphaCurve;
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
    private float _baseGameVolumeWeightBackup;

    private void Awake()
    {
        _aim = GetComponent<PlayerAim>();
        _shield = GetComponent<Shield>();
        _rigidbody = GetComponent<Rigidbody>();
        _mobTarget = GetComponent<MobTarget>();
        _movement = GetComponent<PlayerMovement>();
        _interact = GetComponent<PlayerInteract>();
        _qteControler = GetComponent<QTEControler>();
        _weaponControler = GetComponent<WeaponControler>();

        _playerDisolve = GetComponent<PlayerDisolve>();

        _shield.OnDeathEvent.AddListener(Respawn);

        _baseGameVolumeWeightBackup = _baseGameVolume.weight;
    }

    private void Respawn()
    {
        _qteControler.KillQTE();

        if (ScreenHider.instance)
        {
            StartCoroutine(DisableAllForDuration(_gameplayDisableDuration));
            ScreenHider.instance?.HideScreenForDuration(1, .3f, () =>
            {
                _playerDisolve.SetMatValue(0);
                _rigidbody.MovePosition(_respawnPoint.transform.position);
                _rigidbody.velocity = Vector3.zero;

                _baseGameVolume.weight = 0;
                _deathVolume.weight = 1;
                _canvasGroup.alpha = 0;
            },
            () =>
            {
                _playerDisolve.ShowPlayerWithDisolve(_gameplayDisableDuration - 1 - .3f);
                _respawnPoint.StartVisualSequence(_gameplayDisableDuration - 1 - .3f);

                DOTween.To((time) =>
                {
                    _baseGameVolume.weight = Mathf.Lerp(0, _baseGameVolumeWeightBackup, time);
                    _deathVolume.weight = Mathf.Lerp(1, 0, time);
                    _canvasGroup.alpha = Mathf.Lerp(0, 1, _canvasAlphaCurve.Evaluate(time));
                }, 0, 1, _gameplayDisableDuration - 1 - .3f);
            });
        }
        else
        {
            StartCoroutine(DisableAllForDuration(_gameplayDisableDuration));
            _rigidbody.MovePosition(_respawnPoint.transform.position);
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


