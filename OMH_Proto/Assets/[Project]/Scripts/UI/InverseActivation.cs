using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InverseActivation : MonoBehaviour
{
    [SerializeField] private GameObject _objectToModify;
    [SerializeField] private Image _imageToModify;
    [SerializeField] private TextMeshProUGUI _textToModify;
    
    public void InverseActivationObject()
    {
        _objectToModify.SetActive(!_objectToModify.activeSelf);
    }
    
    public void InverseActivationComponent()
    {
        _imageToModify.enabled = !_imageToModify.IsActive();
    }
    
    public void InverseActivationText()
    {
        _textToModify.enabled = !_textToModify.IsActive();
    }
}
