using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TestOnMouseOver : MonoBehaviour ,IPointerClickHandler
{
    [SerializeField] private Camera _mapCamera;

    [SerializeField] private LayerMask _layerMask;
    private Vector3 worldPos;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        float tempFloatx = Mathf.InverseLerp(-300, 300, transform.InverseTransformPoint(eventData.position).x);
        float tempFloaty = Mathf.InverseLerp(-300, 300, transform.InverseTransformPoint(eventData.position).y);

        float x = Mathf.Lerp(-_mapCamera.orthographicSize, _mapCamera.orthographicSize, tempFloatx);
        float y = Mathf.Lerp(-_mapCamera.orthographicSize, _mapCamera.orthographicSize, tempFloaty);

        worldPos = _mapCamera.transform.TransformPoint(new Vector3(x, y, 0));

        Physics.Raycast(worldPos, Vector3.down, out RaycastHit ray, 100, _layerMask);
        if (!ray.collider) return;
        Debug.DrawRay(worldPos, Vector3.down, Color.green);

        IMapClickable mapClickable = ray.collider.GetComponent<IMapClickable>();
        if (mapClickable != null)
        {
            mapClickable.OnClick();
        }
    }

    public void OnDrawGizmos()
    {
        // Gizmos.DrawSphere(worldPos, 20);
    }
}
