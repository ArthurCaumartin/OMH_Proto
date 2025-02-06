using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapMouseOver : MonoBehaviour ,IPointerClickHandler, IPointerMoveHandler
{
    [SerializeField] private Camera _mapCamera;

    [SerializeField] private LayerMask _layerMask;

    public GameObject _playerPos;
    public Texture2D texture;
    public int lenghtDiscover;
    
    private Texture _rawImage;
    private Vector3 worldPos;

    private void Start()
    {
        _rawImage = GetComponent<RawImage>().texture;

        for (int i = 0; i < texture.height; i++)
        {
            for (int j = 0; j < texture.width; j++)
            {
                texture.SetPixel(j, i, Color.black);
            }
        }
        texture.Apply();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePos = GetWorldPos(eventData);
        
        Physics.Raycast(mousePos, Vector3.down, out RaycastHit ray, 100, _layerMask);
        if (!ray.collider) return;

        IMapClickable mapClickable = ray.collider.GetComponent<IMapClickable>();
        if (mapClickable != null)
        {
            mapClickable.OnClick();
        }
        
        // texture.SetPixels(0, 0, 20, 20, new Color[1]);
    }

    // public void OnDrawGizmos()
    // {
    //     Gizmos.DrawRay(worldPos, new Vector3(0, -1000, 0));
    // }

    Vector3 GetWorldPos(PointerEventData eventData)
    {
        float tempFloatx = Mathf.InverseLerp(-_rawImage.width / 2, _rawImage.width / 2, transform.InverseTransformPoint(eventData.position).x);
        float tempFloaty = Mathf.InverseLerp(-_rawImage.height / 2, _rawImage.height / 2, transform.InverseTransformPoint(eventData.position).y);
        
        float x = Mathf.Lerp(-_mapCamera.orthographicSize, _mapCamera.orthographicSize, tempFloatx);
        float y = Mathf.Lerp(-_mapCamera.orthographicSize, _mapCamera.orthographicSize, tempFloaty);

        worldPos = _mapCamera.transform.TransformPoint(new Vector3(x, y, 0));
        
        return worldPos;
    }

    Vector2 GetTexturePos(PointerEventData eventData)
    {
        float tempFloatx = Mathf.InverseLerp(-texture.width, texture.width, transform.InverseTransformPoint(eventData.position).x);
        float tempFloaty = Mathf.InverseLerp(-texture.height, texture.height, transform.InverseTransformPoint(eventData.position).y);
        
        float x = Mathf.Lerp(0, texture.width, tempFloatx);
        float y = Mathf.Lerp(0, texture.height, tempFloaty);
        
        return new Vector2(x, y);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Vector2 vector2 = GetTexturePos(eventData);
        
        for (int i = -lenghtDiscover; i < lenghtDiscover; i++)
        {
            for (int j = -lenghtDiscover; j < lenghtDiscover; j++)
            {
                texture.SetPixel((int)vector2.x + i, (int)vector2.y + j, new Color(0, 0, 0, 0));
            }
        }
        texture.Apply();
    }
}
