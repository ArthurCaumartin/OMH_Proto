using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntaliumManager : MonoBehaviour
{
    [SerializeField, Tooltip("When checked, Metal gain is logarithmic, when not, it's linear")] private bool _isBuildB;
    [Space]
    [SerializeField] private GameEvent _gainMetal;
    [SerializeField] private FloatReference _syntalium;
    [SerializeField] private float _baseTimerValue = 0.85f;

    [SerializeField] private int _maxGenerators = 6;
    [SerializeField] private float _maxSyntaliumPerSecond = 6;
    
    
    private int _numberOfGenerators;
    private float _timeToGetOneSyntalium;
    private float _timer;

    public void ActivateGenerator()
    {
        _numberOfGenerators++;
        if (_isBuildB)
        {
            _timeToGetOneSyntalium = 1 - ((1 - (1 / _maxSyntaliumPerSecond)) * Mathf.Log(_numberOfGenerators) / Mathf.Log(_maxGenerators));
        }
        else _timeToGetOneSyntalium = 1 - (0.2f * _numberOfGenerators);
    }

    private void Update()
    {
        if (_numberOfGenerators <= 0) return;
        
        _timer += Time.deltaTime;
        if (_timer >= _timeToGetOneSyntalium)
        {
            // _syntalium.Value++;
            
            _gainMetal.Raise();
            
            _timer = 0;
        }
    }
}
