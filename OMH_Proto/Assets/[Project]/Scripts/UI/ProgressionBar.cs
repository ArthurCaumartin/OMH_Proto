using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBar : MonoBehaviour
{
    [SerializeField] private Image _fillSlider;
    
    private float _currentValue;
    private float _timer;
    private bool _isDefenseStarted;

    private void Update()
    {
        if (!_isDefenseStarted) return;
        
        _timer += Time.deltaTime;
        _currentValue = _timer / 180f;
        
        if(_currentValue != 0) _fillSlider.fillAmount = _currentValue;
    }

    public void StartDefense()
    {
        _isDefenseStarted = true;
    }
}
