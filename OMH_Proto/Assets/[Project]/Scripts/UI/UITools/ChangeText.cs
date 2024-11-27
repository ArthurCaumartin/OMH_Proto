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
    [SerializeField] private bool _isColoredGreen;
    
    private float _memoryValue;
    
    private void Start()
    {
        _memoryValue = _valueToChange.Value;
        
        ChangeTextInCanvas();
    }

    public void ChangeTextInCanvas()
    {
        if (_valueToChange.Value != _memoryValue && _isColoredGreen)
        {
            _textToChange.color = new Color32(22, 184, 0, 255);
        }
        _textToChange.text = _baseText + _valueToChange.Value;
    }
}
