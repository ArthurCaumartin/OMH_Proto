using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CursorMovement : MonoBehaviour
{
    [SerializeField] private GameObject _parentCursor;
    [SerializeField] private RectTransform _fillImage;
    [SerializeField] private AnimationCurve _rotateCurve;
    [SerializeField] private FloatReference _explorationTime, _defenseTime;

    float maxTime = 0;
    float currentTime = 0;
    
    private void Start()
    {
        maxTime = _explorationTime.Value + _defenseTime.Value;
    }

    private void Update()
    {
        float degree = Mathf.Lerp(0, 360, Mathf.InverseLerp(0, maxTime, currentTime)) * Mathf.Deg2Rad;

        _parentCursor.transform.up = new Vector3(Mathf.Sin(degree), Mathf.Cos(degree), 0);

        currentTime += Time.deltaTime;
    }
}
