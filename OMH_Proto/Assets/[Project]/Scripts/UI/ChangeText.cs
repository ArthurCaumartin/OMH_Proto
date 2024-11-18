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

    private void Start()
    {
        ChangeTextInCanvas();
    }

    public void ChangeTextInCanvas()
    {
        // print("Change Text");
        
        _textToChange.text = _baseText + _valueToChange.Value;
    }
}
