using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum MovementAnimationState { Idle, Runing, Walking }
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private MovementAnimationState _currentMovementAnimationState;
    [SerializeField] private Transform _meshPivot;
    [SerializeField] private WeaponControler _weaponControler;
    [SerializeField] private float _speed = 5;
    private PlayerMovement _playerMovement;
    private PlayerAim _playerAim;
    private bool _isPlayerShooting;
    public bool IsPlayerShooting { set => _isPlayerShooting = value; }


    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAim = GetComponent<PlayerAim>();
    }

    private void Update()
    {
        _meshPivot.transform.localPosition = Vector3.zero; //! le mesh bouge tout seul /:
        SetMovementState(DefineState());
        _animator.SetLayerWeight(1, 0);

        _animator.SetBool("IsShooting", _isPlayerShooting);

        switch (_currentMovementAnimationState)
        {
            case MovementAnimationState.Idle:
                IdleUpdate();
                break;

            case MovementAnimationState.Walking:
                WalkingUpdate();
                break;

            case MovementAnimationState.Runing:
                RunningUpdate();
                break;
        }
    }

    private void IdleUpdate()
    {
        Vector3 directionTarget = _playerAim.GetAimDirection();
        _meshPivot.forward = Vector3.Lerp(_meshPivot.forward
                                        , directionTarget == Vector3.zero ? _meshPivot.forward : directionTarget
                                        , Time.deltaTime * _speed);
    }

    private void WalkingUpdate()
    {
        Vector3 directionTarget = _playerAim.GetAimDirection();
        _meshPivot.forward = Vector3.Lerp(_meshPivot.forward
                                        , directionTarget
                                        , Time.deltaTime * _speed);

        //! Localise la direction des Input avec l'orientation du Mesh
        Vector3 absolutInput = _playerMovement.GetMovementDirection();
        Vector3 moveInPivot = _meshPivot.transform.rotation * new Vector3(absolutInput.x, 0, absolutInput.z);
        if (Mathf.Abs(absolutInput.x) > .2f) moveInPivot.z *= -1; //! rearange orientation moveInPivot is inverted on X ?!

        // print($"Input = {_playerMovement.GetMovementDirection()} // Localise Input = {moveInPivot}");
        // Debug.DrawRay(transform.position + Vector3.up, _playerMovement.GetMovementDirection() * 10, Color.red);
        // Debug.DrawRay(transform.position + Vector3.up, moveInPivot * 5, Color.cyan);

        _animator.SetLayerWeight(1, absolutInput.magnitude);

        _animator.SetFloat("InputLocal_MouseX", Mathf.Lerp(-1, 1, Mathf.InverseLerp(-1, 1, moveInPivot.x)));
        _animator.SetFloat("InputLocal_MouseZ", Mathf.Lerp(-1, 1, Mathf.InverseLerp(-1, 1, moveInPivot.z)));
    }

    private void RunningUpdate()
    {
        Vector3 directionTarget = _playerMovement.GetMovementDirection();
        _meshPivot.forward = Vector3.Lerp(_meshPivot.forward
                                        , directionTarget == Vector3.zero ? _meshPivot.forward : directionTarget
                                        , Time.deltaTime * _speed);

    }

    private MovementAnimationState DefineState()
    {
        if (_weaponControler.IsShooting()) return MovementAnimationState.Walking;
        if (_playerMovement.GetMovementDirection() != Vector3.zero && !_weaponControler.IsShooting()) return MovementAnimationState.Runing;
        return MovementAnimationState.Idle;
    }

    private void SetMovementState(MovementAnimationState stateToSet)
    {
        if (_currentMovementAnimationState == stateToSet) return;
        _currentMovementAnimationState = stateToSet;

        _animator.SetBool("IsIdle", _currentMovementAnimationState == MovementAnimationState.Idle);
        _animator.SetBool("IsRunning", _currentMovementAnimationState == MovementAnimationState.Runing);
        _animator.SetBool("IsWalking", _currentMovementAnimationState == MovementAnimationState.Walking);
    }

    public void SetWeaponAnimation(int stateHash)
    {
        _animator.Play(stateHash);
    }
}
