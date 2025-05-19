using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoSliderPannel : MonoBehaviour
{
    [SerializeField] private float _timeToDisapear;
    [SerializeField] private GameObject _tutoParent;
    [SerializeField] private Slider _slider;
    
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        _slider.value = _timer / _timeToDisapear;
        if (_timer >= _timeToDisapear)
        {
            _tutoParent.SetActive(false);
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1;
    }
}
