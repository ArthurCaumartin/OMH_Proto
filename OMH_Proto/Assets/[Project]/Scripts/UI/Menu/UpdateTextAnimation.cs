using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateTextAnimation : MonoBehaviour
{
    [SerializeField] private float _countFPS = 30;
    [SerializeField] private float _duration = 1;
    
    private int _valueTest;
    private Coroutine _coroutine;
    private string NumberFormat = "N0";
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _valueTest = 0;
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

        // if (value - previousValue >= 1000)
        // {
        //     _countFPS = 100;
        //     _duration = 1;
        // }
        // else if (value - previousValue >= 10)
        // {
        //     _countFPS = 10;
        //     _duration = 10;
        // }
        // else
        // {
        //     _countFPS = value - previousValue;
        //     _duration = 20;
        // }
        
        // float tempValue = value - previousValue;
        // if(tempValue > 1000) tempValue = 1000;
        // float invLerpValue = Mathf.InverseLerp(1, 1000, tempValue);
        //
        // _countFPS = Mathf.Lerp(100, value - previousValue, invLerpValue);
        // _duration = Mathf.Lerp(1, 10, invLerpValue);
        
        if(value - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((value - previousValue) / _countFPS);
        }
        else
        {
            stepAmount = Mathf.CeilToInt((value - previousValue) / _countFPS);
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
                
                yield return new WaitForSeconds(0.01f * _duration);
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
                
                yield return new WaitForSeconds(0.01f * _duration);
            }
        }

        _valueTest = previousValue;
    }
}
