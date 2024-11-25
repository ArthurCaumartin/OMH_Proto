using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FillImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private FloatReference _fillDuration;
    [SerializeField] private AnimationCurve _animationCurve;

    private float tempFloat = 0.35f;

    public void StartFillImage()
    {
        DOTween.To(()=> tempFloat, x=> tempFloat = x, 1, _fillDuration.Value).SetEase(_animationCurve);
        
    //     float tempFloat = (float) ((double) _defenseDuration.Value / 10);
    //     float tempFloat2 = (float) ((double) 1 / tempFloat);
    //     _animator.speed = tempFloat2;
    }

    public void Update()
    {
        _image.fillAmount = tempFloat;
    }
}
