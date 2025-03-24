using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPin : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeTallMap()
    {
        gameObject.layer = LayerMask.NameToLayer("Map");
    }

    public void ChangeLittleMap()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void ChangeSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
