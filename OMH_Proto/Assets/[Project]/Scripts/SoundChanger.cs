using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundChanger : MonoBehaviour
{
    [SerializeField] private FloatReference _soundVolume;
    private AudioSource _audioSource;
    
    private float volume;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        volume = _soundVolume.Value / 100f;
        
        _audioSource.volume = volume;
    }

    private void Update()
    {
        if (_soundVolume.Value != volume)
        {
            UpdateVolume();
        }
    }

    private void UpdateVolume()
    {
        volume = _soundVolume.Value / 100f;
        _audioSource.volume = volume;
    }
}
