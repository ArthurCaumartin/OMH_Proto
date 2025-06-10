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
        if(objectName != null) _objectName.text = objectName;
        if(objectDescription != null) _objectDescription.text = objectDescription;
        if(objectImage != null) _objectImage.sprite = objectImage;

        HighLightObject highLightObject = GetComponent<HighLightObject>();
        if(highLightObject != null) highLightObject.InitializeInfos(_objectName.text, _objectDescription.text, _objectImage.sprite);
    }

    public void InitalizeEndGame(Sprite objectImage)
    {
        _objectImage.sprite = objectImage;
    }
}
