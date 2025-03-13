using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Splines;


[ExecuteInEditMode]
public class SplineTransition : MonoBehaviour
{
    public bool DEBUG = false;
    public float DEBUG_SIZE = 5;
    [Serializable]
    public class SplineState
    {
        public string name;
        [Range(0, 1)] public float time;
        public Transform lookAtTarget;
        public Color debugColor = Color.blue;
    }
    [SerializeField] private int _currentIndexTarget;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _rotateSpeed = 5;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private List<SplineState> _splineStateList;
    private float _currentTime;
    private float _dampVelocity;

    private void OnValidate()
    {
        if (_currentIndexTarget > _splineStateList.Count - 1) _currentIndexTarget = _splineStateList.Count;
        if (_currentIndexTarget < 0) _currentIndexTarget = 0;
    }

    private void Update()
    {
        if (_currentIndexTarget > _splineStateList.Count - 1 || _currentIndexTarget < 0) return;
        if (!_splineContainer || _splineStateList.Count == 0 || !_cameraTransform) return;

        float timeTarget = _splineStateList[_currentIndexTarget].time;
        // _currentTime = Mathf.Lerp(_currentTime, timeTarget, Time.deltaTime * _speed);
        _currentTime = Mathf.SmoothDamp(_currentTime, timeTarget, ref _dampVelocity, 1 / _moveSpeed, Mathf.Infinity);

        MoveCamera(_currentTime);
        RotateCamera(_currentIndexTarget);
    }

    private void MoveCamera(float time)
    {
        Vector3 posTarget = _splineContainer[0].EvaluatePosition(time);
        posTarget = transform.TransformPoint(posTarget);
        _cameraTransform.position = posTarget;
    }

    private void RotateCamera(int index)
    {
        Vector3 lookAtTargetDir = (_splineStateList[index].lookAtTarget.position - _cameraTransform.position).normalized;
        _cameraTransform.forward = Vector3.Slerp(_cameraTransform.forward, lookAtTargetDir, Time.deltaTime * _rotateSpeed);
        // Quaternion targetRotation = Quaternion.LookRotation(lookAtTargetDir);
        // _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, targetRotation, Time.deltaTime * _speed);
    }

    private void OnDrawGizmos()
    {
        if(!DEBUG) return;
        if (!_splineContainer || _splineStateList.Count == 0) return;
        foreach (var item in _splineStateList)
        {
            // print("100% gaming mon pote !!!!");
            Vector3 worldPos = _splineContainer[0].EvaluatePosition(item.time);
            worldPos = transform.TransformPoint(worldPos);
            Gizmos.color = item.debugColor;
            Gizmos.DrawSphere(worldPos, DEBUG_SIZE);
        }
    }

    public void SetIndex(int index)
    {
        _currentIndexTarget = index;
    }
}
