using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDoorTempSealed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText1, _timerText2;
    [SerializeField]private int _seconds = 30;
    
    private float _timer;
    
    private void Awake()
    {
        _timerText1.text = _seconds.ToString();
        _timerText2.text = _seconds.ToString();
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            _seconds--;
            _timer = 0;
            _timerText1.text = _seconds.ToString();
            _timerText2.text = _seconds.ToString();
        }
    }
}
