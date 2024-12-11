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
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _speed = 5;
    private PlayerMovement _playerMovement;
    private PlayerAim _playerAim;
    private Vector3 _mouseDirection;


    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAim = GetComponent<PlayerAim>();
    }


    private void Update()
    {
        _meshPivot.transform.localPosition = Vector3.zero; //! le mesh bouge tout seul /:
        _mouseDirection = _playerAim.GetAimDirection();
        Debug.DrawRay(transform.position, _mouseDirection * 3f, Color.red);


        SetState(DefineState());
        print("Anim State : " + _currentAnimationState);


        _animator.SetLayerWeight(1, 0);
        _animator.SetLayerWeight(2, 0);


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


    }

    private void WalkingUpdate()
    {
        float mouseDotForward = Vector3.Dot(transform.forward, _playerAim.GetAimDirection());
        float mouseDotRight = Vector3.Dot(transform.right, _playerAim.GetAimDirection());
        print("Forward : " + mouseDotForward + " // Right : " + mouseDotRight);

        Vector3 directionTarget = _playerAim.GetAimDirection();
        _meshPivot.forward = Vector3.Lerp(_meshPivot.forward
                                        , directionTarget
                                        , Time.deltaTime * _speed);

        if (_playerMovement.GetMovementDirection() != Vector3.zero)
        {
            //! dois definire les layer avec la direction 'absolue' des inputs
            //! dois definire les weight en fonction de l'orientation des 
            _animator.SetLayerWeight(1, Mathf.Lerp(0, 1, Mathf.InverseLerp(-1, 1, _meshPivot.forward.x)));
            _animator.SetLayerWeight(2, Mathf.Lerp(0, 1, Mathf.InverseLerp(-1, 1, _meshPivot.forward.z)));

            _animator.SetFloat("MouseX", Mathf.Lerp(0, 1, Mathf.InverseLerp(-1, 1, _meshPivot.forward.x)));
            _animator.SetFloat("MouseY", Mathf.Lerp(0, 1, Mathf.InverseLerp(-1, 1, _meshPivot.forward.z)));
        }
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
        if (_weapon.IsPlayerShooting()) return AnimationState.Walking;
        if (_playerMovement.GetMovementDirection() != Vector3.zero && !_weapon.IsPlayerShooting()) return AnimationState.Runing;
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
