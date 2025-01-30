using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayout : MonoBehaviour
{
    [SerializeField] private LayerMask _aimLayer;
    [SerializeField] private FloatReference _range;
    public Camera _mainCamera;
    private Transform _playerTransform;
    private Vector3 _currentMousePos;

    public Material _GridMaterial;

    private void Start()
    {
        _playerTransform = FindAnyObjectByType<PlayerMovement>()?.transform;
        if (!_playerTransform) enabled = false;
    }

    private void Update()
    {
        _currentMousePos = MouseAimPosition(_currentMousePos);
        Vector2 mousePos = new Vector2(_currentMousePos.x, _currentMousePos.z);

        _GridMaterial.SetVector("_CursorLocation", mousePos);
    }

    private Vector3 MouseAimPosition(Vector3 currentPos)
    {
        Vector2 pixelPos = Input.mousePosition;
        Ray camRay = _mainCamera.ScreenPointToRay(pixelPos);

        Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity, _aimLayer);
        if (!hit.collider) return currentPos;

        // if (DEBUG) Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.green);
        // if (DEBUG) Debug.DrawLine(new Vector3(hit.point.x, hit.point.y - 1, hit.point.z)
        //                         , new Vector3(hit.point.x, hit.point.y + 1, hit.point.z)
        //                         , Color.red);

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
