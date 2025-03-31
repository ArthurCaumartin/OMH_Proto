using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerGame : MonoBehaviour
{
    [SerializeField] private FloatReference _exploDuration;

    [SerializeField] private Color _colorTextDefense;
    
    private TextMeshProUGUI _timerText;
    private float _timer;
    private int _intCounter;

    private bool _isDefenseStarted;
    
    void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        _timerText.color = Color.white;
        
        _intCounter = (int)_exploDuration.Value;
        _timer = _intCounter;


        if ((_intCounter % 60) <= 10)
        {
            if (_intCounter < 600) _timerText.text = $"0{((int)_exploDuration.Value - _intCounter) / 60}:0{((int)_exploDuration.Value - _intCounter) % 60}";
            else _timerText.text = $"{((int)_exploDuration.Value - _intCounter) / 60}:0{((int)_exploDuration.Value - _intCounter) % 60}";
        }
        else _timerText.text = $"0{((int)_exploDuration.Value - _intCounter) / 60}:{((int)_exploDuration.Value - _intCounter) % 60}";
    }

    private void Update()
    {
        if (_isDefenseStarted)
        {
            _timer += Time.deltaTime;
            if (_timer > _intCounter)
            {
                if (_intCounter % 60 < 10)
                {
                    if (_intCounter < 600) _timerText.text = $"0{_intCounter / 60}:0{_intCounter % 60}";
                    else _timerText.text = $"{_intCounter / 60}:0{_intCounter % 60}";
                }
                else
                {
                    if (_intCounter < 600) _timerText.text = $"0{_intCounter / 60}:{_intCounter % 60}";
                    else _timerText.text = $"0{_intCounter / 60}:{_intCounter % 60}";
                }
                _intCounter++;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
            if (_timer < _intCounter)
            {
                if (_intCounter % 60 < 10)
                {
                    if(_intCounter < 600) _timerText.text = $"0{_intCounter / 60}:0{_intCounter % 60}";
                    else _timerText.text = $"{_intCounter / 60}:0{_intCounter % 60}";
                }
                else
                {
                    if(_intCounter < 600) _timerText.text = $"0{_intCounter / 60}:{_intCounter % 60}";
                    else _timerText.text = $"{_intCounter / 60}:{_intCounter % 60}";
                }
                _intCounter--;
            }
        }
        
        if (_intCounter + 1 == 0)
        {
            StartDefense();
            // Destroy(gameObject);
        }
    }

    public void StartDefense()
    {
        _isDefenseStarted = true;
        _timerText.color = _colorTextDefense;
        _timerText.text = "00:00";
        _timer = 0;
        _intCounter = 0;
    }
}
