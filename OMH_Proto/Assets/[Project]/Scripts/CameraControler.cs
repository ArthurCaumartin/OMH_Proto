using System;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    [Header("Moving Parametre :")]
    [SerializeField] private bool _dampMovement = true;
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _inputFollowStrengh = 2f;
    [Header("Data Set up :")]
    [SerializeField] private Vector3 _posOffset;
    private Vector3 _velocity;
    private Vector3 _inputOffSet;

    private void Start()
    {
        if (!_camera) _camera = Camera.main;
    }

    private void OnValidate()
    {
        // if (_target) FollowTarget(false);
    }

    private void Update()
    {
        FollowTarget(_dampMovement);
    }

    private void FollowTarget(bool dampMovement)
    {
        if (dampMovement)
        {
            Vector3 targetPos = _target.position + _posOffset + new Vector3(_inputOffSet.x, 0, _inputOffSet.y);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, 1 / _followSpeed, Mathf.Infinity);
        }
        else
        {
            transform.position = _target.position + _posOffset + new Vector3(_inputOffSet.x, 0, _inputOffSet.y);
        }
    }

    public void SetInputOffSet(Vector2 offSet)
    {
        _inputOffSet = offSet * _inputFollowStrengh;
    }

    // public void SetCameraOnState(PartyState state)
    // {
    // switch (state)
    // {
    //     case PartyState.Mobile:
    //         DOTween.To((time) =>
    //         {
    //             transform.eulerAngles = Vector3.Lerp(_towerdefenceData.angle, _movingData.angle, time);
    //             _posOffset = Vector3.Lerp(_towerdefenceData.offSet, _movingData.offSet, time);
    //         }, 0, 1, _transitionDuration)
    //         .SetEase(Ease.Linear);
    //         break;

    //     case PartyState.TowerDefence | PartyState.TowerDefencePlacement:
    //         DOTween.To((time) =>
    //         {
    //             transform.eulerAngles = Vector3.Lerp(_movingData.angle, _towerdefenceData.angle, time);
    //             _posOffset = Vector3.Lerp(_movingData.offSet, _towerdefenceData.offSet, time);
    //         }, 0, 1, _transitionDuration)
    //         .SetEase(Ease.Linear);
    //         break;
    // }
    // }

    //! ///////////////////////////////////////////////////
    //! ///////////////////////////////////////////////////
    //! CALL BY EDITOR CLASS
    public void LookAtTarget()
    {
        if (_target)
            transform.LookAt(_target);
    }

    public void Reset()
    {
        _posOffset = Vector3.zero;
        transform.rotation = Quaternion.identity;
        OnValidate();
    }
    //! ///////////////////////////////////////////////////
    //! ///////////////////////////////////////////////////
}
