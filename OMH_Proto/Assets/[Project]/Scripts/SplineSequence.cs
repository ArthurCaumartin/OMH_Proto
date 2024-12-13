using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineSequence : MonoBehaviour
{
    [Serializable]
    public struct Sequence
    {
        public float speed;
        public SplineContainer spline;
        public Transform toLook;
    }

    [SerializeField] private Transform _objectToMove;
    [SerializeField] private List<Sequence> _splineSequences;
    private float _distance;
    private int _currentIndex;

    void Update()
    {
        if (_splineSequences.Count == 0) return;
        _distance += Time.deltaTime * _splineSequences[_currentIndex].speed;

        MoveOnSpline(_currentIndex, _distance);

        if (_distance >= _splineSequences[_currentIndex].spline[0].GetLength())
        {
            _distance = 0;
            _currentIndex++;
            if (_currentIndex >= _splineSequences.Count) _currentIndex = 0;
        }
    }

    private void MoveOnSpline(int index, float distance)
    {
        float time = distance / _splineSequences[index].spline[0].GetLength();

        _objectToMove.transform.position =
        _splineSequences[index].spline.transform
        .TransformPoint(_splineSequences[index].spline[0].EvaluatePosition(time));

        if (_splineSequences[index].toLook)
            _objectToMove.LookAt(_splineSequences[index].toLook);
        else
            _objectToMove.transform.forward = _splineSequences[index].spline[0].EvaluateTangent(time);
    }
}
