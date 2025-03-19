using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPin : MonoBehaviour
{
    [SerializeField] public Sprite _littleMapPin, _tallMapPin;

    private SpriteRenderer _spriteRenderer;
    
    private bool _isMapTall;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeTallMap()
    {
        
        gameObject.layer = LayerMask.NameToLayer("TallMap");
        
        // _isMapTall = true;
        // _spriteRenderer.sprite = _tallMapPin;
    }

    public void ChangeLittleMap()
    {
        gameObject.layer = LayerMask.NameToLayer("LittleMap");
        
        // _isMapTall = false;
        // if (_littleMapPin == null)
        // {
        //     _spriteRenderer.sprite = null;
        // }
        // else
        // {
        //     _spriteRenderer.sprite = _littleMapPin;
        // }
    }
}
