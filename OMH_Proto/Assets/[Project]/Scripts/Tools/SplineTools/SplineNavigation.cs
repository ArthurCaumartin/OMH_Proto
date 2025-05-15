using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class SplineNavigation : MonoBehaviour
{
    [SerializeField] private Transform _transformToMove;
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private float _speed;
    [SerializeField] private float _minSpeed = .01f;

    [Header("Interface :")]
    [SerializeField] private Image _timerImage;
    [SerializeField] private float _hideSpeed = 5;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _backButton;
    [Space]
    public List<Node> _nodes = new List<Node>();
    private int _currentIndex;

    private float _currentTime;
    private float _timeTarget;
    private float _currentWaitingTime;


    [Serializable]
    public class Node
    {
        public string name;
        public Color debug_Color = new Color(1, 0, 0, .5f);
        public float debug_Size = 1f;
        public float distance;
        public float waitingTime;
    }

    private void Start()
    {
        _nextButton.onClick.AddListener(() => { MoveCurrentIndex(1); });
        _backButton.onClick.AddListener(() => { MoveCurrentIndex(-1); });

        // set position on start
        _currentTime = _nodes[_currentIndex].distance / _splineContainer[0].GetLength();
        Vector3 pos = _splineContainer[0].EvaluatePosition(_currentTime);
        pos = _splineContainer.transform.InverseTransformPoint(pos);
        _transformToMove.position = pos + _offSet;
    }

    private void Update()
    {
        _timeTarget = _nodes[_currentIndex].distance / _splineContainer[0].GetLength();

        // print("Lerp Time : " + Time.deltaTime * _speed / (_timeTarget - _currentTime));
        LerpCurrentTarget(_timeTarget);

        Vector3 pos = _splineContainer[0].EvaluatePosition(_currentTime);
        pos = _splineContainer.transform.InverseTransformPoint(pos);
        _transformToMove.position = pos + _offSet;

        WaitLogics();
    }

    private void WaitLogics()
    {
        if (Mathf.Abs(_timeTarget - _currentTime) < .01f)
        {
            _currentWaitingTime += Time.deltaTime;
            _timerImage.fillAmount = _currentWaitingTime / _nodes[_currentIndex].waitingTime;
            if (_currentWaitingTime >= _nodes[_currentIndex].waitingTime)
            {
                _currentWaitingTime = 0;
                MoveCurrentIndex(1);
            }
        }
        else
        {
            _currentWaitingTime = 0;
            _timerImage.fillAmount -= Time.deltaTime * _hideSpeed;
        }
    }

    private void LerpCurrentTarget(float timeTarget)
    {
        float ditance = Mathf.Abs(timeTarget - _currentTime);
        float time = Time.deltaTime * _speed / Mathf.Clamp(ditance, _minSpeed, Mathf.Infinity);
        _currentTime = Mathf.Lerp(_currentTime, timeTarget, time);
    }

    private void MoveCurrentIndex(int dir)
    {
        if (dir < 0 && _currentIndex == 0) return;
        if (dir > 0 && _currentIndex >= _nodes.Count - 1) return;
        _currentIndex += dir;
    }

    private void OnDrawGizmos()
    {
        if (_nodes.Count == 0 || !_splineContainer) return;

        foreach (var item in _nodes)
        {
            Gizmos.color = item.debug_Color;
            Vector3 pos = _splineContainer.transform.InverseTransformPoint(
                            _splineContainer[0].EvaluatePosition(item.distance / _splineContainer[0].GetLength()));
            Gizmos.DrawSphere(pos, item.debug_Size);
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if (input == Vector2.zero) return;
        MoveCurrentIndex((int)Mathf.Abs(input.x));
    }
}