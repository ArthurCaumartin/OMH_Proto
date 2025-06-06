using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpdateVolumeLevels : MonoBehaviour
{
    public UnityEngine.UI.Slider _volSlider;
    [SerializeField] private WwiseSoundSettings.VolumeType _volumeType;

    private void Start()
    {
        if (_volSlider == null)
        _volSlider = GetComponent<UnityEngine.UI.Slider>();
        _volSlider.value = WwiseSoundSettings.Instance.GetVolume(_volumeType);
    }

    public void UpdateVolume()
    {
        WwiseSoundSettings.Instance.SetVolume(_volumeType, _volSlider.value);
    }
}

