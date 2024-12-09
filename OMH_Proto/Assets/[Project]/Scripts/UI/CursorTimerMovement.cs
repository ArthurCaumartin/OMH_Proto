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

    private void Start()
    {
        MoveCursor();
    }

    private void MoveCursor()
    {
        Sequence timerCursorSequence = DOTween.Sequence();

        timerCursorSequence.Append(
            _parentCursor.transform
                .DORotate(new Vector3(transform.rotation.x, transform.rotation.y, 60), _explorationTime.Value 
                + _defenseTime.Value)
            .SetEase(_rotateCurve));

        timerCursorSequence.Append(
            _parentCursor.transform
                .DORotate(new Vector3(transform.rotation.x, transform.rotation.y, 0), 0.01f));

        float tempX = 283.2935f, tempY = 124.9183f;
        Vector2 fillImageSize = new Vector2(tempX, tempY);
        timerCursorSequence.Append(_fillImage.DOSizeDelta(fillImageSize, 10, false).SetEase(_rotateCurve));
    }

    private void Update()
    {
        // _fillImage.sizeDelta = fillImageSize;
    }
}
