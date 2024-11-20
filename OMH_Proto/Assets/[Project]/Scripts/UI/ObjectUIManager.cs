using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _objectImagePrefab;
    private Image _imageComponent;
    private bool _isPannelActivated;

    private void Start()
    {
        _imageComponent = GetComponent<Image>();
    }

    private void Update()
    {
        if (_imageComponent.IsActive())
        {
            _isPannelActivated = true;
        }
        else
        {
            _isPannelActivated = false;
        }
    }

    public void AddObjectUI(string objectName, string objectDescription, Sprite objectImage)
    {
        ObjectUI objectUI = Instantiate(_objectImagePrefab, transform).GetComponent<ObjectUI>();

        objectUI.Initialize(objectName, objectDescription, objectImage, _isPannelActivated);
    }
}
