using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponTimerShot : MonoBehaviour
{
    [SerializeField] private GameObject _uiWeaponParent;
    [SerializeField] private Image _fillImage, _weaponImage;

    private float _secondaryTimer;
    private float _fillAmout;
    private bool _isFired;

    private void Start()
    {
        _fillAmout = 1;
    }

    public void InitializeWeapon(Weapon weapon)
    {
        _weaponImage.sprite = weapon._weaponVisual._weaponSprite;
        _secondaryTimer = weapon._secondaryCooldown.Value;

        if (_secondaryTimer <= 0) _uiWeaponParent.SetActive(false);
        else _uiWeaponParent.SetActive(true);
        
        _fillImage.fillAmount = 1;
    }
    
    public void SecondaryShot()
    {
        if (_isFired) return;
        
        _isFired = true;
        
        _fillImage.fillAmount = 0;
        _fillAmout = 0;
        DOTween.To(() => _fillAmout, x => _fillAmout = x, 1f, _secondaryTimer)
            .SetEase(Ease.Linear)
            .OnComplete(OnCompleteTimer);
    }

    void OnCompleteTimer()
    {
        _isFired = false;
    }

    private void Update()
    {
        _fillImage.fillAmount = _fillAmout;
    }
}
