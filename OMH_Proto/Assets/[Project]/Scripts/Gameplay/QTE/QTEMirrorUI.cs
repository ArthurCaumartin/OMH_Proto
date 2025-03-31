using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class QTEMirrorUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _reproduceButtonsParent, _endText;
    
    [SerializeField] private Image _objectsToLight;
    [SerializeField] private List<GameObject> _reproduceObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _activateObjects = new List<GameObject>();
    
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private Sprite _reproduceButtonSprite, _notReproduceButtonSprite;

    public void InitializeUI(int numberWinsValue)
    {
        _canvas.enabled = true;
        // _canvas.worldCamera = Camera.main.GetUniversalAdditionalCameraData().cameraStack[Camera.main.GetUniversalAdditionalCameraData().cameraStack.Count - 1];
        _counterText.text = $"1 / {numberWinsValue}";
        
        
        _objectsToLight.color = new Color32(29, 173, 215, 255);
        
    }
    
    public void NewQTE(int[] intArray, int winCounter, int numberWinsValue)
    {
        _counterText.text = $"{winCounter + 1} / {numberWinsValue}";
        
        for (int i = 0; i < _activateObjects.Count; i++)
        {
            _activateObjects[i].GetComponent<Image>().color = new Color32(29, 173, 215, 255);
            // _reproduceObjects[i].GetComponent<Image>().sprite = _notReproduceButtonSprite;
            _reproduceObjects[i].GetComponent<Image>().color = new Color32(29, 173, 215, 255);
        }
        
        for (int i = 0; i < intArray.Length; i++)
        {
            Image tempImage = _reproduceObjects[intArray[i]].GetComponent<Image>();
            tempImage.color = Color.green;
            // tempImage.sprite = _reproduceButtonSprite;
        }
    }

    public void ResetQTE()
    {
        _canvas.enabled = false;
        
        for (int i = 0; i < _activateObjects.Count; i++)
        {
            _activateObjects[i].GetComponent<Image>().color = new Color32(29, 173, 215, 255);
            _reproduceObjects[i].GetComponent<Image>().color = new Color32(29, 173, 215, 255);
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
            _activateObjects[i].GetComponent<Image>().color = new Color32(29, 173, 215, 255);
        }
        StartCoroutine(BadInputFeedBack());
    }

    public void WinCode()
    {
        _reproduceButtonsParent.SetActive(false);
        _endText.SetActive(true);
    }

    private IEnumerator BadInputFeedBack()
    {
        _objectsToLight.color = new Color32(232, 73, 73, 255);
        
        yield return new WaitForSeconds(0.75f);
        
        _objectsToLight.color = new Color32(29, 173, 215, 255);
    }
}
