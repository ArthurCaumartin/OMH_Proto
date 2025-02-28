using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CreditsMoving : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _moveSpeed;

    private Vector3 _startPosition;
    private float _timer;
    
    private void Start()
    {
        _startPosition = _rectTransform.position;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y + 1 * Time.deltaTime * _moveSpeed);
    }
}
