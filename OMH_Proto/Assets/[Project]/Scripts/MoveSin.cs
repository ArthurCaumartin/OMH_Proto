using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour
{
    [SerializeField] private Vector3 _axis = new Vector3(0, 1, 0);
    [SerializeField] private float _amplitude = 5;
    [SerializeField] private float _speed = 5;
    float _offSet = 0;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    private void Update()
    {
        _offSet = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.localPosition = _startPos + (_axis * _offSet);
    }





}
