using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonMenuHighlight : Selectable, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Sprite _highLightSprite, _normalSprite;
    [SerializeField] private Color _highLightColor, _normalColor;
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    
    [SerializeField] private UnityEvent _onClick;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = _highLightSprite;
        _text.color = _highLightColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = _normalSprite;
        _text.color = _normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick.Invoke();
        _image.sprite = _normalSprite;
        _text.color = _normalColor;
    }
}
