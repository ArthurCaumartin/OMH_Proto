using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEDoorUI : MonoBehaviour
{
    [SerializeField] private Image _circleImage;
    [SerializeField] private Canvas _mainCanvas;
    
    public void BadInput()
    {
        _circleImage.color = new Color32(255, 0, 0, 255);
    }
    
    public void ActivateUI()
    {
        _mainCanvas.enabled = true;
    }
    
    public void ResetUI()
    {
        _mainCanvas.enabled = false;
    }
}
