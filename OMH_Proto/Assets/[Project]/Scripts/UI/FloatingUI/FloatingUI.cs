using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    [SerializeField] private Transform _worldLink;
    [SerializeField] private RectTransform _background;
    [SerializeField] private LineRenderer _topLine;
    [SerializeField] private LineRenderer _botLine;
    private Transform _cameraTransform;

    private Vector3 _startLocalPosition;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _startLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.LookAt(_cameraTransform);

        // transform.position = 

        Vector3 worldPosBackTop = _background.TransformPoint(new Vector3(_background.rect.width / 2, _background.rect.height / 2, 0));
        _topLine.SetPosition(0, worldPosBackTop);
        _topLine.SetPosition(1, _worldLink.position);
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(_background.position, _background.rotation, _background.localScale);
        Gizmos.color = new Color(1, 0, 0, .2f);
        Gizmos.DrawSphere(new Vector3(_background.rect.width / 2, _background.rect.height / 2, 0), .1f);
    }
}

