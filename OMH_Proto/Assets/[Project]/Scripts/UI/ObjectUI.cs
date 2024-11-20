using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectName, _objectDescription;
    [SerializeField] private Image _objectImage, _popupImage;

    public void Initialize(string objectName, string objectDescription, Sprite objectImage, bool isPannelActivated)
    {
        _objectName.text = objectName;
        _objectDescription.text = objectDescription;
        _objectImage.sprite = objectImage;
        _popupImage.sprite = objectImage;
        if (!isPannelActivated)
        {
            _objectImage.enabled = false;
        }
    }
}
