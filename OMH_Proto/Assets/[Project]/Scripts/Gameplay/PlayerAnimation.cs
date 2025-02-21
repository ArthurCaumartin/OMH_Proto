using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum AnimationState { Idle, Runing, Walking }
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private AnimationState _currentAnimationState;
    [SerializeField] private Transform _meshPivot;
    [SerializeField] private WeaponControler _weaponControler;
    [SerializeField] private float _speed = 5;
    private PlayerMovement _playerMovement;
    private PlayerAim _playerAim;


    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAim = GetComponent<PlayerAim>();
    }


    private void Update()
    {
        _meshPivot.transform.localPosition = Vector3.zero; //! le mesh bouge tout seul /:

        SetState(DefineState());
        // print("Anim State : " + _currentAnimationState);


        _animator.SetLayerWeight(1, 0);


        switch (_currentAnimationState)
        {
            case AnimationState.Idle:
                IdleUpdate();
                break;

            case AnimationState.Walking:
                WalkingUpdate();
                break;

            case AnimationState.Runing:
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

    private AnimationState DefineState()
    {
        if (_weaponControler.IsPlayerShooting()) return AnimationState.Walking;
        if (_playerMovement.GetMovementDirection() != Vector3.zero && !_weaponControler.IsPlayerShooting()) return AnimationState.Runing;
        return AnimationState.Idle;
    }

    private void SetState(AnimationState stateToSet)
    {
        if (_currentAnimationState == stateToSet) return;
        _currentAnimationState = stateToSet;

        _animator.SetBool("IsIdle", _currentAnimationState == AnimationState.Idle);
        _animator.SetBool("IsRunning", _currentAnimationState == AnimationState.Runing);
        _animator.SetBool("IsWalking", _currentAnimationState == AnimationState.Walking);
    }
}
