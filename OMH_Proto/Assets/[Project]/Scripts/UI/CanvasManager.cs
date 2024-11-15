using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactText;
    
    public void InteractText()
    {
        if (_interactText.enabled)
        {
            _interactText.enabled = false;
        }
    }
    
    public void DisplayText()
    {
        _interactText.enabled = !_interactText.enabled;
    }
}
