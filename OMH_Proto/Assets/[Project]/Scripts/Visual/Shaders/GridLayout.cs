using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayout : MonoBehaviour
{
    [SerializeField] private LayerMask _aimLayer;
    [SerializeField] private FloatReference _range;
    public Camera _mainCamera;
    private Transform _playerTransform;
    private Vector3 _currentMousePos = Vector3.zero;
    private Vector3 _localMousePos = Vector3.zero;
    private Vector2 _normalizePos= Vector2.zero;

    public Material _GridMaterial;

    private void Start()
    {
        _playerTransform = FindAnyObjectByType<PlayerMovement>()?.transform;
        if (!_playerTransform) enabled = false;
    }

    private void Update()
    {
        _currentMousePos = MouseAimPosition(_currentMousePos);
        
        _localMousePos = transform.InverseTransformPoint(_currentMousePos);

        float scale = transform.localScale.x;
        _normalizePos.x = Mathf.InverseLerp(-5, 5, _localMousePos.x) - (.5f);
        _normalizePos.y = Mathf.InverseLerp(-5, 5, _localMousePos.z) - (.5f);

        _GridMaterial.SetVector("_CursorLocation", _normalizePos);
    }

    private Vector3 MouseAimPosition(Vector3 currentPos)
    {
        Vector2 pixelPos = Input.mousePosition;
        Ray camRay = _mainCamera.ScreenPointToRay(pixelPos);

        Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity, _aimLayer);

        if (!hit.collider) return currentPos;

        Debug.DrawRay(camRay.origin, camRay.direction, Color.green);
        Debug.DrawLine(new Vector3(hit.point.x, hit.point.y - 1, hit.point.z)
                                , new Vector3(hit.point.x, hit.point.y + 1, hit.point.z)
                                , Color.red);

        // Vector3 normalizePos = new Vector3(Mathf.Lerp(transform.localScale.x))

        if (Vector3.Distance(_playerTransform.position, hit.point) > _range.Value)
        {
            return _playerTransform.position + (hit.point - _playerTransform.position).normalized * _range.Value;
        }
        else
        {
            return hit.point;
        }
    }
}
