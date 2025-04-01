using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SyringeUI : MonoBehaviour
{
    [SerializeField] private Image _syringeImage;
    [SerializeField] private GameObject _syringeContainer;
    [SerializeField] private FloatReference _syringeValue;

    private float _tempFloat;
    private Sprite _syringeEmptyIcon;
    private void Start()
    {
        _tempFloat = _syringeValue.Value;
        _syringeEmptyIcon = _syringeImage.sprite;
    }
    
    void Update()
    {
        if (_tempFloat != _syringeValue.Value && _syringeValue.Value > 0)
        {
            _syringeContainer.SetActive(true);
            _tempFloat = _syringeValue.Value;
        }
        else if (_tempFloat != _syringeValue.Value && _syringeValue.Value == 0)
        {
            _syringeImage.sprite = _syringeEmptyIcon;
            _tempFloat = _syringeValue.Value;
        }
    }
}
