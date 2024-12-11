using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SyringeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _syringeText;
    [SerializeField] private Image _syringeImage;
    [SerializeField] private FloatReference _syringeValue;
    [SerializeField] private Sprite _syringeFilledIcon;

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
            _syringeText.text = "Charged " + _syringeValue.Value + " charges";
            _syringeImage.sprite = _syringeFilledIcon;
            _tempFloat = _syringeValue.Value;
        }
        else if (_tempFloat != _syringeValue.Value && _syringeValue.Value == 0)
        {
            _syringeText.text = "Empty\n" + _syringeValue.Value + " charge";
            _syringeImage.sprite = _syringeEmptyIcon;
            _tempFloat = _syringeValue.Value;
        }
    }
}
