using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
    [SerializeField] private FloatVariable _valueToChange;
    [SerializeField] private TextMeshProUGUI _textToChange;

    [SerializeField] private string _baseText;
    [SerializeField] private bool _isColoredGreen, _isReverse;
    
    private float _memoryValue;

    public void Initialize()
    {
        _memoryValue = _valueToChange.Value;
        ChangeTextInCanvas(); 
    }

    public void ChangeTextInCanvas()
    {
        if (_valueToChange.Value != _memoryValue && _isColoredGreen)
        {
            _textToChange.color = new Color32(100, 174, 50, 255);
        }
        else if(_valueToChange.Value == _memoryValue && _isColoredGreen)
        {
            _textToChange.color = new Color32(252, 217, 89, 255);
        }

        if (!_isReverse)
        {
            _textToChange.text = _baseText + _valueToChange.Value;
        }
        else
        {
            _textToChange.text = _valueToChange.Value + _baseText;
        }
    }

    public void Update()
    {
        ChangeTextInCanvas();
    }
}
