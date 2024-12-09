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
        _mouseDirection = _playerAim.GetAimDirection();
        Debug.DrawRay(transform.position, _mouseDirection * 3f, Color.red);
        Vector3 directionTarget = _playerMovement.GetMovementDirection();
        _meshPivot.forward = Vector3.Lerp(_meshPivot.forward
                                        , directionTarget == Vector3.zero ? _meshPivot.forward : directionTarget
                                        , Time.deltaTime * _speed);

        SetState(DefineState());
        print("Anim State : " + _currentAnimationState);
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
        float maxTime = 0;
        float currentTime = 0;
        float degree = Mathf.Lerp(0, 360, Mathf.InverseLerp(0, maxTime, currentTime)) * Mathf.Deg2Rad;

        new Vector3(Mathf.Sin(degree), Mathf.Cos(degree), 0);

        currentTime += Time.deltaTime;
    }

    private void WalkingUpdate()
    {

    }

    private void RunningUpdate()
    {

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
