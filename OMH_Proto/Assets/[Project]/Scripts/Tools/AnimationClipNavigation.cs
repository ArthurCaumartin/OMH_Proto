using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StepData
{
    [HideInInspector] public string name;
    public float length;
    public float startSecond;
    public float startTime;

    public StepData(string name, float length, float startSecond, float startTime = 0)
    {
        this.name = name;
        this.length = length;
        this.startSecond = startSecond;
        this.startTime = startTime;
    }
}

public class AnimationClipNavigation : MonoBehaviour
{
    [SerializeField] private Slider _sliderUI;
    [SerializeField] private float _speed = .2f;
    [SerializeField, Range(0, 1)] private float _sliderTime;
    [SerializeField] private Animator _animator;
    private float _totalAnimationDuration;
    private List<StepData> _stepDataList = new List<StepData>();
    private int _currentStepIndex;
    private bool _isAutoSlider = true;

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
                    _stepDataList.Add(new StepData(tempClips[j].name, _totalAnimationDuration, _totalAnimationDuration));
                    _totalAnimationDuration += tempClips[j].length;
                }
            }
        }

        // set normalize time in a other loop because need total duration
        for (int i = 0; i < rac.animationClips.Length; i++)
            _stepDataList[i].startTime = Mathf.InverseLerp(0, _totalAnimationDuration, _stepDataList[i].startSecond);
    }

    void Update()
    {
        if (_isAutoSlider)
        {
            _sliderTime += Time.deltaTime * _speed;
            if (_sliderTime >= 1) _sliderTime = 0;
            _sliderUI.value = _sliderTime;
        }
        else
        {
            _sliderTime = _sliderUI.value;
        }

        _currentStepIndex = GetIndexOnSliderTime(_stepDataList, _sliderTime);
        if (_currentStepIndex == -1)
        {
            print("-1 !!!!!!!!!!!!");
            return;
        }

        _animator.Play(_stepDataList[_currentStepIndex].name, 0, GetClipTime(_stepDataList, _currentStepIndex, _sliderTime));
    }

    private float GetClipTime(List<StepData> data, int index, float time)
    {
        float maxRemapValue = 0;
        if (index + 1 >= data.Count)
            maxRemapValue = 1;
        else
            maxRemapValue = data[index + 1].startTime;

        return Mathf.InverseLerp(data[index].startTime, maxRemapValue, time);
    }

    private int GetIndexOnSliderTime(List<StepData> data, float sliderTime)
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
                // print($"Clip {data[i].name} index = {i}");
                return i;
            }
        }
        return -1;
    }

    public void EnableAutoSlider(bool value)
    {
        _isAutoSlider = value;
    }
}
