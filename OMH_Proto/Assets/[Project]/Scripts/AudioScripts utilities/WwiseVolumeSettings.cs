using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;
using System;

public class WwiseSoundSettings : MonoBehaviour
{
    public static WwiseSoundSettings Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeDictionaries();
        InitializeVolumeSettings();
    }

    [Serializable]
    public class VolumeSetting
    {
        public VolumeType Option;
        public RTPC WwiseRTPC;
        public float Volume;
    }

    public enum VolumeType
    {
        Master,
        MusicGen,
        MusicMenu,
        MusicGame,
        SFX,
        PlayerSounds,
        Ambience,
        UI,
        VO
    }

    public VolumeSetting[] volumeSettings;

    private Dictionary<VolumeType, VolumeSetting> settingsDict;

    private void InitializeDictionaries()
    {
        settingsDict = new Dictionary<VolumeType, VolumeSetting>();
        foreach (var setting in volumeSettings)
        {
            settingsDict[setting.Option] = setting;
        }
    }

    public void SetVolume(VolumeType type, float value)
    {
        if (settingsDict.TryGetValue(type, out var setting))
        {
            setting.WwiseRTPC.SetGlobalValue(value);
            PlayerPrefs.SetFloat(type.ToString(), value);
        }
        else
        {
            Debug.LogWarning($"Volume setting '{type}' not found.");
        }
    }

    public float GetVolume(VolumeType type)
    {
        if (settingsDict.TryGetValue(type, out var setting))
        {
            return PlayerPrefs.GetFloat(type.ToString(), setting.Volume);
        }
        Debug.LogWarning($"Volume setting '{type}' not found.");
        return 0f;
    }

    private void Start()
    {
        ApplyVolumeSettings();
    }

    private void InitializeVolumeSettings()
    {
        bool hasSavedSettings = false;
        foreach (var setting in settingsDict.Values)
        {
            if (PlayerPrefs.HasKey(setting.Option.ToString()))
            {
                hasSavedSettings = true;
                break;
            }
        }

        if (hasSavedSettings)
        {
            LoadVolumeSettings();
        }
        else
        {
            ApplyDefaultVolumeSettings();
        }
    }

    private void ApplyDefaultVolumeSettings()
    {
        foreach (var setting in settingsDict.Values)
        {
            setting.WwiseRTPC.SetGlobalValue(setting.Volume);
        }
    }

    private void LoadVolumeSettings()
    {
        foreach (var setting in settingsDict.Values)
        {
            float value = PlayerPrefs.GetFloat(setting.Option.ToString(), setting.Volume);
            setting.WwiseRTPC.SetGlobalValue(value);
        }
    }

    private void ApplyVolumeSettings()
    {
        foreach (var setting in settingsDict.Values)
        {
            float value = GetVolume(setting.Option);
            setting.WwiseRTPC.SetGlobalValue(value);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}