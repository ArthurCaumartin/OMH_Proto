using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapMouseOver : MonoBehaviour ,IPointerClickHandler
{
    [SerializeField] private Camera _mapCamera;

    [SerializeField] private LayerMask _layerMask;
    
    private Texture _rawImage;
    private Vector3 worldPos;

    private void Start()
    {
        _rawImage = GetComponent<RawImage>().texture;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePos = GetWorldPos(eventData);
        
        Physics.Raycast(worldPos, Vector3.down, out RaycastHit ray, 100, _layerMask);
        if (!ray.collider) return;

        IMapClickable mapClickable = ray.collider.GetComponent<IMapClickable>();
        if (mapClickable != null)
        {
            mapClickable.OnClick();
        }
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
}
