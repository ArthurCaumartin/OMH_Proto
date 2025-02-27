using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerGame : MonoBehaviour
{
    [SerializeField] private FloatReference _exploDuration;

    private TextMeshProUGUI _timerText;
    private float _timer;
    private int _intCounter;
    
    void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        
        _intCounter = (int)_exploDuration.Value;
        _timer = _intCounter;
        
        
        if ((_intCounter % 60) <= 10) _timerText.text = $"0{((int)_exploDuration.Value - _intCounter) / 60}:0{((int)_exploDuration.Value - _intCounter) % 60}";
        else _timerText.text = $"0{((int)_exploDuration.Value - _intCounter) / 60}:{((int)_exploDuration.Value - _intCounter) % 60}";
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < _intCounter)
        {
            if (_intCounter % 60 < 10)
            {
                _timerText.text = $"0{_intCounter / 60}:0{_intCounter % 60}";
            }
            else
            {
                _timerText.text = $"0{_intCounter / 60}:{_intCounter % 60}";
            }
            _intCounter--;
        }
        
        if (_intCounter + 1 == 0)
        {
            Destroy(gameObject);
        }
    }
}
