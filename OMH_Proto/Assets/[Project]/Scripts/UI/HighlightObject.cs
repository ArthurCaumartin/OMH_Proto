using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighLightObject: Selectable, IPointerClickHandler
{
    public GameObject _infoPopup;
    [SerializeField] private bool _isClickable;
    private bool isHighlighted;
    [SerializeField] private UnityEvent m_OnClick;
    
    // If the object is HighLighted in Canvas, appear an info pop-up
    void Update()
    {
        if (IsHighlighted() && !isHighlighted)
        {
            if(_infoPopup) _infoPopup.SetActive(true);
            isHighlighted = true;
        }
        else if(!IsHighlighted() && isHighlighted)
        {
            if(_infoPopup) _infoPopup.SetActive(false);
            isHighlighted = false;
        }
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!_isClickable) return;
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Press();
    }
    private void Press()
    {
        if (!IsActive() || !IsInteractable())
            return;

        // UISystemProfilerApi.AddMarker("Button.onClick", this);
        m_OnClick.Invoke();
    }
}