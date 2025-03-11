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
        StartCoroutine(BadInputCoroutine());
    }

    private IEnumerator BadInputCoroutine()
    {
        _circleImage.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.9f);
        _circleImage.color = new Color32(255, 255, 255, 255);
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
