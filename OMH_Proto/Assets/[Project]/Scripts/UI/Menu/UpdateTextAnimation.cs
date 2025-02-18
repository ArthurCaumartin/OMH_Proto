using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateTextAnimation : MonoBehaviour
{
    [SerializeField] private int _countFPS = 30;
    [SerializeField] private float _duration = 1;
    
    private int _valueTest;
    private Coroutine _coroutine;
    private string NumberFormat = "N0";
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeTextAnimation(int newValue)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        StartCoroutine(CountText(newValue));
    }

    private IEnumerator CountText(int value)
    {
        yield return new WaitForSeconds(1f / _countFPS);
        int previousValue = _valueTest;
        int stepAmount;
        
        if(value - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((value - previousValue) / (_countFPS * _duration));
        }
        else
        {
            stepAmount = Mathf.CeilToInt((value - previousValue) / (_countFPS * _duration));
        }

        if (previousValue < value)
        {
            while (previousValue < value)
            {
                previousValue += stepAmount;
                if (previousValue > value)
                {
                    previousValue = value;
                }
                
                _text.SetText(previousValue.ToString(NumberFormat));
                
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (previousValue > value)
            {
                previousValue += stepAmount;
                if (previousValue < value)
                {
                    previousValue = value;
                }
                
                _text.SetText(previousValue.ToString(NumberFormat));
                
                yield return new WaitForSeconds(0.01f);
            }
        }

        _valueTest = previousValue;
    }
}
