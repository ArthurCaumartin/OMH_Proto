using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SlideData
{
    [HideInInspector] public string name;
    public float length;
    public float startSecond;
    public float startTime;

    public SlideData(string name, float length, float startSecond, float startTime = 0)
    {
        this.name = name;
        this.length = length;
        this.startSecond = startSecond;
        this.startTime = startTime;
    }
}

public class SlideAnimator : MonoBehaviour
{
    [SerializeField] private Slider _sliderUI;
    [SerializeField] private float _speed = .2f;
    [SerializeField, Range(0, 1)] private float _sliderTime;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _currentSecond;
    [SerializeField] private float _totalDuration;
    [SerializeField] private List<SlideData> _slideData;
    [SerializeField] private int _currentIndex;
    private bool _autoSlider = true;

    private void Start()
    {
        InitializeSlideData();
    }

    private void InitializeSlideData()
    {
        RuntimeAnimatorController rac = _animator.runtimeAnimatorController;
        List<AnimationClip> tempClips = rac.animationClips.ToList();
        int clipCount = tempClips.Count;
        int lastFound = 0;

        for (int i = 0; i < clipCount; i++)
        {
            for (int j = 0; j < tempClips.Count; j++)
            {
                int currentIndex = Convert.ToInt32(tempClips[j].name.Split("_")[1]);
                if (currentIndex == lastFound + 1)
                {
                    lastFound = currentIndex;
                    _slideData.Add(new SlideData(tempClips[j].name, _totalDuration, _totalDuration));
                    _totalDuration += tempClips[j].length;
                }
            }
        }

        // set normalize time in a other loop because need total duration
        for (int i = 0; i < rac.animationClips.Length; i++)
            _slideData[i].startTime = Mathf.InverseLerp(0, _totalDuration, _slideData[i].startSecond);
    }

    void Update()
    {
        if (_autoSlider)
        {
            _sliderTime += Time.deltaTime * .2f;
            if (_sliderTime >= 1) _sliderTime = 0;
            _sliderUI.value = _sliderTime;
        }
        else
        {
            _sliderTime = _sliderUI.value;
        }

        _currentIndex = GetIndexOnSliderTime(_slideData, _sliderTime);
        if (_currentIndex == -1)
        {
            print("-1 !!!!!!!!!!!!");
            return;
        }

        _animator.Play(_slideData[_currentIndex].name, 0, GetClipTime(_currentIndex));
    }

    private float GetClipTime(int index)
    {
        float maxRemapValue = 0;
        if (index + 1 >= _slideData.Count)
            maxRemapValue = 1;
        else
            maxRemapValue = _slideData[index + 1].startTime;

        return Mathf.InverseLerp(_slideData[index].startTime, maxRemapValue, _sliderTime);
    }

    private int GetIndexOnSliderTime(List<SlideData> data, float sliderTime)
    {
        for (int i = 0; i <= data.Count; i++)
        {
            if (i > data.Count)
                return -1;

            if (i == data.Count - 1)
            {
                return data.Count - 1;
            }

            if (data[i + 1].startTime > sliderTime)
            {
                print($"Clip {data[i].name} index = {i}");
                return i;
            }
        }
        return -1;
    }

    public void EnableAutoSlider(bool value)
    {
        _autoSlider = value;
    }
}
