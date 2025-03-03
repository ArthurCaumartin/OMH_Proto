using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private FloatReference _masterVolume, _soundEffectsVolume, _musicVolume;
    [SerializeField] private Slider _masterVolumeSlider, _soundEffectsVolumeSlider, _musicVolumeSlider;
    [SerializeField] private TextMeshProUGUI _masterVolumeText, _soundEffectsVolumeText, _musicVolumeText;
    [Space]
    [SerializeField] private bool _isFullScreen; 
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Toggle _fullScreenToggle;
    [SerializeField] private TextMeshProUGUI _fullScreenText;
    private UnityEngine.Resolution[] _resolutions;
    List<UnityEngine.Resolution> _selectedResolutions = new List<UnityEngine.Resolution>();
    int _selectedResolution;
    // [Space]
    //Accessibility

    private void Start()
    { 
        //Sound
        _masterVolumeText.text = $"{_masterVolume.Value}";
        _masterVolumeSlider.value = _masterVolume.Value;
        
        _soundEffectsVolumeText.text = $"{_soundEffectsVolume.Value}";
        _soundEffectsVolumeSlider.value = _soundEffectsVolume.Value;
        
        _musicVolumeText.text = $"{_musicVolume.Value}";
        _musicVolumeSlider.value = _musicVolume.Value;
        
        //Screen
        _fullScreenText.text = _isFullScreen ? "ON" : "OFF";
        _isFullScreen = Screen.fullScreen;
        _resolutions = Screen.resolutions;
        List<string> resolutionsStringList = new List<string>();
        string newRes;
        
        foreach (UnityEngine.Resolution res in _resolutions)
        {
            newRes = res.width + " x " + res.height;
            if (!resolutionsStringList.Contains(newRes))
            {
                resolutionsStringList.Add(newRes);
                _selectedResolutions.Add(res);
            }
        }
        _resolutionDropdown.AddOptions(resolutionsStringList);
    }
    
    private void Update()
    {
        if (_masterVolumeSlider.value != _masterVolume.Value)
        {
            ChangeMasterVolume(_masterVolumeSlider.value);
        }
        if (_soundEffectsVolumeSlider.value != _soundEffectsVolume.Value)
        {
            ChangeSoundEffectsVolume(_soundEffectsVolumeSlider.value);
        }
        if (_musicVolumeSlider.value != _musicVolume.Value)
        {
            ChangeMusicVolume(_musicVolumeSlider.value);
        }
    }

    //Screen
    public void ChangeFullScreen()
    {
        _isFullScreen = _fullScreenToggle.isOn;
        Screen.SetResolution(_selectedResolutions[_selectedResolution].width, _selectedResolutions[_selectedResolution].height, _isFullScreen);
        _fullScreenText.text = _isFullScreen ? "ON" : "OFF";
    }

    public void ChangeResolution()
    {
        _selectedResolution = _resolutionDropdown.value;
        Screen.SetResolution(_selectedResolutions[_selectedResolution].width, _selectedResolutions[_selectedResolution].height, _isFullScreen);
    }
    
    //Sound
    public void ChangeMasterVolume(float newValue)
    {
        _masterVolumeText.text = $"{newValue}";
        _masterVolume.Value = newValue;
    }
    public void ChangeSoundEffectsVolume(float newValue)
    {
        _soundEffectsVolumeText.text = $"{newValue}";
        _soundEffectsVolume.Value = newValue;
    }
    public void ChangeMusicVolume(float newValue)
    {
        _musicVolumeText.text = $"{newValue}";
        _musicVolume.Value = newValue;
    }
}

[Serializable]
public class Resolution
{
    public int width;
    public int height;
}
