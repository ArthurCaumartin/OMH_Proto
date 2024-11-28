using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InverseActivation : MonoBehaviour
{
    [SerializeField] private GameObject _objectToModify;
    [SerializeField] private MaskableGraphic _imageToModify;
    
    public void InverseActivationObject()
    {
        _objectToModify.SetActive(!_objectToModify.activeSelf);
    }
    
    public void InverseActivationComponent()
    {
        _imageToModify.enabled = !_imageToModify.IsActive();
    }
}
