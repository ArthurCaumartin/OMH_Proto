using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapFogOfWar : MonoBehaviour
{
    [SerializeField] private Camera _mapCamera;
    [SerializeField] private LayerMask _layerMask;
    [Space]
    [SerializeField] private GameObject _playerPos, _texturePos;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private int _circleRadius;
    
    private Vector2 relativePosPlayer; 

    private void Awake()
    {
        for (int i = 0; i < _texture.height; i++)
        {
            for (int j = 0; j < _texture.width; j++)
            {
                _texture.SetPixel(j, i, Color.black);
            }
        }
        _texture.Apply();

        for (int i = 0; i < _circleRadius; i++)
        {
            TracePixel(GetPos(), i, new Color(0,0,0,0));
        }
    }

    //It's working but it cost a lot
    private void Update()
    {
        TracePixel(GetPos(), _circleRadius, new Color(0,0,0,0));
        TracePixel(GetPos(), _circleRadius - 1, new Color(0,0,0,0));
        TracePixel(GetPos(), _circleRadius - 2, new Color(0,0,0,0));
    }

    public void TracePixelRoom(Transform roomPos, int radius)
    {
        Vector3 relativePos = _texturePos.transform.InverseTransformPoint(roomPos.transform.position);
        
        float tempFloatx = Mathf.InverseLerp(-0.5f, 0.5f, relativePos.x);
        float tempFloaty = Mathf.InverseLerp(-0.5f, 0.5f, relativePos.y);
        
        float x = Mathf.Lerp(0, _texture.width, tempFloatx);
        float y = Mathf.Lerp(0, _texture.height, tempFloaty);

        Vector2 tempPos = new Vector2(x, y);
        
        for (int i = -radius / 2; i < radius / 2; i++)
        {
            for (int j = -radius / 2; j < radius / 2; j++)
            {
                _texture.SetPixel((int)tempPos.x + i, (int)tempPos.y +j, new Color(0, 0, 0, 0));
            }
        }
        _texture.Apply();
    }

    Vector2 GetPos()
    {
        Vector3 relativePos = _texturePos.transform.InverseTransformPoint(_playerPos.transform.position);
        
        float tempFloatx = Mathf.InverseLerp(-0.5f, 0.5f, relativePos.x);
        float tempFloaty = Mathf.InverseLerp(-0.5f, 0.5f, relativePos.y);
        
        float x = Mathf.Lerp(0, _texture.width, tempFloatx);
        float y = Mathf.Lerp(0, _texture.height, tempFloaty);

        relativePosPlayer = new Vector2(x, y);
        
        return relativePosPlayer;
    }

    void TracePixel(Vector2 pixelPos, int radius, Color color)
    {
        int r = radius;
        int deltaR = 0;
        int x = r;
        int y = 0;

        DrawPixel(pixelPos, r, 0, color);
        DrawPixel(pixelPos, 0, r, color);

        while (x > y)
        {
            int delta1 = deltaR + 2 * y - 2 * x + 2;
            int delta2 = deltaR + 2 * y + 1;

            if (Mathf.Abs(delta1) < Mathf.Abs(delta2))
            {
                deltaR = delta1;
                y++;
                x--;
            }
            else
            {
                deltaR = delta2;
                y++;
            }
            
            DrawPixel(pixelPos, x, y, color);
            DrawPixel(pixelPos, y, x, color);
        }
        
        
        _texture.Apply();
    }

    void DrawPixel(Vector2 center, int x, int y, Color color)
    {
        int x0 = (int)center.x;
        int y0 = (int)center.y;
        if(_texture.GetPixel(x0 + x, y0 + y) != color) _texture.SetPixel(x0 + x, y0 + y, color);
        if(_texture.GetPixel(x0 + x, y0 - y) != color) _texture.SetPixel(x0 + x, y0 - y, color);
        if(_texture.GetPixel(x0 - x, y0 + y) != color) _texture.SetPixel(x0 - x, y0 + y, color);
        if(_texture.GetPixel(x0 - x, y0 - y) != color) _texture.SetPixel(x0 - x, y0 - y, color);
    }
}
