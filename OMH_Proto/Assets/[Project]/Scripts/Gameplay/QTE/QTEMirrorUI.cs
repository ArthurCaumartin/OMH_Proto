using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class QTEMirrorUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _reproduceButtonsParent, _endText;
    
    [SerializeField] private List<GameObject> _reproduceObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _activateObjects = new List<GameObject>();
    
    [SerializeField] private TextMeshProUGUI _counterText;

    public void InitializeUI(int numberWinsValue)
    {
        _canvas.enabled = true;
        _canvas.worldCamera = Camera.main.GetUniversalAdditionalCameraData().cameraStack[Camera.main.GetUniversalAdditionalCameraData().cameraStack.Count - 1];
        _counterText.text = $"1 / {numberWinsValue}";
    }
    
    public void NewQTE(int[] intArray, int winCounter, int numberWinsValue)
    {
        _counterText.text = $"{winCounter + 1} / {numberWinsValue}";
        
        for (int i = 0; i < _activateObjects.Count; i++)
        {
            _activateObjects[i].GetComponent<Image>().color = Color.red;
            _reproduceObjects[i].GetComponent<Image>().color = Color.red;
        }
        
        for (int i = 0; i < intArray.Length; i++)
        {
            Image tempImage = _reproduceObjects[intArray[i]].GetComponent<Image>();
            tempImage.color = Color.green;
        }
    }

    public void ResetQTE()
    {
        _canvas.enabled = false;
        
        for (int i = 0; i < _activateObjects.Count; i++)
        {
            _activateObjects[i].GetComponent<Image>().color = Color.red;
            _reproduceObjects[i].GetComponent<Image>().color = Color.red;
        }
    }
    
    public void SetGoodInputFeedBack(int value)
    {
        _activateObjects[value].GetComponent<Image>().color = Color.green;
    }

    public void SetBadInputFeedBack()
    {
        for (int i = 0; i < _activateObjects.Count; i++)
        {
            _activateObjects[i].GetComponent<Image>().color = Color.red;
        }
    }

    public void WinCode()
    {
        _reproduceButtonsParent.SetActive(false);
        _endText.SetActive(true);
    }
}
