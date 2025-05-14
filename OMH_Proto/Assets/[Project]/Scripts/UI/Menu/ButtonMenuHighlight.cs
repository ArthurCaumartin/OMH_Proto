using System;
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

    [SerializeField] public AK.Wwise.Event _enterAreaSound;
    [SerializeField] private float _timeBeforeTrigSound = 0.14f;
    public bool _cursorIsIn;
    private IEnumerator _myCoroutine;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _cursorIsIn = true;
        DelegateSoundHoover();
        _image.sprite = _highLightSprite;
        _text.color = _highLightColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _cursorIsIn = false;
        if (_myCoroutine != null) StopCoroutine(_myCoroutine);
        _image.sprite = _normalSprite;
        _text.color = _normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick.Invoke();
        _image.sprite = _normalSprite;
        _text.color = _normalColor;
    }
    public void DelegateSoundHoover()
    {
        if (_myCoroutine != null) StopCoroutine(_myCoroutine);

        if (_cursorIsIn)
        {
            _myCoroutine = IsInHoover();
            StartCoroutine(_myCoroutine);
        }

    }
    private IEnumerator IsInHoover()
    {
        yield return new WaitForSeconds(_timeBeforeTrigSound);
        if (_cursorIsIn )
        {
            _enterAreaSound.Post(gameObject);
        }
        
    }
}
