using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChooseButton : MonoBehaviour
{
    [SerializeField] private WeaponMeta _weaponMeta;
    [SerializeField] private GameObject _lockObject;
    
    private WeaponChoose _weaponChoose;
    private Button _button;
    private Image _image;
    private TextMeshProUGUI _text;
    
    private bool _isSelected;
    
    private void Awake()
    {
        _weaponChoose = GetComponentInParent<WeaponChoose>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
    }

    public void InitializeUnlocked(WeaponMeta weaponMeta)
    {
        _button.interactable = true;
        _lockObject.SetActive(false);
        _weaponMeta = weaponMeta;
        _text.text = _weaponMeta._weaponName;
    }
    public void InitializeLocked(WeaponMeta weaponMeta)
    {
        _button.interactable = false;
        _lockObject.SetActive(true);
        _weaponMeta = weaponMeta;
        // _text.text = _weaponMeta._weaponName;
    }
    
    public void Click()
    {
        if(_isSelected) return;
        
        _weaponChoose.ClickWeapon(_weaponMeta);
        _isSelected = true;
        _image.color = new Color(0, 1, 0, 1);
    }

    public void Reset()
    {
        _isSelected = false;
        _image.color = new Color(1, 1, 1, 1);
    }
}
