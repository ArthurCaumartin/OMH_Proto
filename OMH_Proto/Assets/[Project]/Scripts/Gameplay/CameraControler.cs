using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    [Header("Moving Parametre :")]
    [SerializeField] private bool _dampMovement = true;
    [SerializeField] private FloatReference _followSpeed;
    [SerializeField] private FloatReference _inputFollowStrengh;
    [Header("Data Set up :")]
    [SerializeField] private Vector3 _posOffset;
    [SerializeField] private Vector3 _rotOffset;
    private Vector3 _posVelocity;
    private Vector3 _rotVelocity;
    private Vector3 _inputOffSet;

    private void Start()
    {
        if (!_camera) _camera = Camera.main;
    }

    private void Update()
    {
        if (!_target) return;
        FollowTarget(_dampMovement);
    }

    private void FollowTarget(bool dampMovement)
    {
        if (dampMovement)
        {
            Vector3 targetPos = _target.position + _posOffset + new Vector3(_inputOffSet.x, 0, _inputOffSet.y);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _posVelocity, 1 / _followSpeed.Value, Mathf.Infinity);
            transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, _rotOffset, ref _rotVelocity, 1 / _followSpeed.Value, Mathf.Infinity);
        }
        else
        {
            transform.position = _target.position + _posOffset + new Vector3(_inputOffSet.x, 0, _inputOffSet.y);
            transform.eulerAngles = _rotOffset;
        }
    }

    public void SetInputOffSet(Vector2 offSet)
    {
        _inputOffSet = offSet * _inputFollowStrengh.Value;
    }

    //! ///////////////////////////////////////////////////
    //! ///////////////////////////////////////////////////
    //! CALL BY EDITOR CLASS
    public void LookAtTarget()
    {
        if (!_target) return;
        transform.LookAt(_target);
    }

    public void SaveOffSet()
    {
        if (!_target) return;
        _posOffset = transform.position - _target.position;
        _rotOffset = transform.eulerAngles;
    }

    public void GoOnTarget()
    {
        if (!_target) return;
        transform.position = _target.position + _posOffset;
        transform.eulerAngles = _rotOffset;
    }

    public void Reset()
    {
        _posOffset = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = _target.position;
    }
    //! ///////////////////////////////////////////////////
    //! ///////////////////////////////////////////////////
}
