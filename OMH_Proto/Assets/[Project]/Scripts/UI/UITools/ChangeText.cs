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
    
    private void Awake()
    {
        ChangeTextInCanvas();
    }

    private void Start()
    {
        ChangeTextInCanvas();
        
        _memoryValue = _valueToChange.Value;
    }

    public void ChangeTextInCanvas()
    {
        if (_valueToChange.Value != _memoryValue && _isColoredGreen)
        {
            _textToChange.color = new Color32(22, 184, 0, 255);
        }
        else if(_valueToChange.Value == _memoryValue && _isColoredGreen)
        {
            _textToChange.color = new Color32(1, 1, 1, 255);
        }
        _textToChange.text = _baseText + _valueToChange.Value;
    }

    public void Update()
    {
        ChangeTextInCanvas();
    }
}
