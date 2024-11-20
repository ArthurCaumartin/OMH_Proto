using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighLightObject: Selectable
{
    public GameObject _infoPopup;
    private bool isHighlighted;
    
    // If the object is HighLighted in Canvas, appear an info pop-up
    void Update()
    {
        if (IsHighlighted() && !isHighlighted)
        {
            _infoPopup.SetActive(true);
            isHighlighted = true;
        }
        else if(!IsHighlighted() && isHighlighted)
        {
            _infoPopup.SetActive(false);
            isHighlighted = false;
        }
    }
}