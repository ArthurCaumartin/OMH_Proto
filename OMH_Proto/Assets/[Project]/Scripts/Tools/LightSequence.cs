using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
public class LightSequence : MonoBehaviour
{
    [SerializeField] private float _cycleDuration;
    [SerializeField] private AnimationCurve _intensityOnCycle;
    [SerializeField] private Gradient _colorOnCycle;
    private float _cycleTime = 0;

    private Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        _cycleTime += Time.deltaTime;
        float time = Mathf.InverseLerp(0, _cycleDuration, _cycleTime);
        _light.intensity = _intensityOnCycle.Evaluate(time);
        _light.color = _colorOnCycle.Evaluate(time);

        if (_cycleTime > _cycleDuration) _cycleTime = 0;
    }
}
