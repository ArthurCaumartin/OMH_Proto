using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FillImage : MonoBehaviour
{
    [Serializable] private enum FillType { NotSet, InverseLerp, Duration }
    [Tooltip("If not set try to get image on Object")]
    [SerializeField] private Image _targetImage;
    [SerializeField] private bool _canFill = true;
    [SerializeField] private FillType _fillType;

    [Header("Inverse Lerp : ")]
    [SerializeField] private FloatReference _minValue;
    [SerializeField] private FloatReference _maxValue;
    [SerializeField] private FloatReference _compareValue;

    [Header("Duration : ")]
    [SerializeField] private FloatReference _maxDuration;
    private float _currentTime;

    private void Start()
    {
        if(!_targetImage) _targetImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (_fillType == FillType.InverseLerp)
        {
            _targetImage.fillAmount = Mathf.InverseLerp(_minValue.Value, _maxValue.Value, _compareValue.Value);
        }

        if (_fillType == FillType.Duration)
        {
            _currentTime += Time.deltaTime;
            _targetImage.fillAmount = Mathf.InverseLerp(0, _maxDuration.Value, _currentTime);
        }
    }

    public void ResetFill()
    {
        _currentTime = 0;
        _targetImage.fillAmount = 0;
    }

    public void EnableFill(bool value)
    {
        _canFill = value;
    }


    // private float tempFloat = 0f;

    // public void StartFillImage()
    // {
    //     DOTween.To(() => tempFloat, x => tempFloat = x, 1, _maxDuration.Value).SetEase(Ease.Linear);

    //     // float tempFloat = (float) ((double) _defenseDuration.Value / 10);
    //     // float tempFloat2 = (float) ((double) 1 / tempFloat);
    //     // _animator.speed = tempFloat2;
    // }

    // public void Update()
    // {
    //     _targetImage.fillAmount = tempFloat;
    // }
}
