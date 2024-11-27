using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Placer : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private FloatReference _range;
    [Space]
    [SerializeField] private FloatVariable _ressourceCondition;
    [Space]
    [SerializeField] private List<Placable> _placableList;
    [SerializeField] private GameEvent _onPlacableSelect;
    private Placable _gostPlacable;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void Select(int index)
    {
        if (_ressourceCondition)
        {
            if (_ressourceCondition.Value - _placableList[index].cost.Value < 0)
                return;
        }

        //* If Player selecte a placable allready select
        if (_gostPlacable)
        {
            UnSelect();
            return;
        }

        if (!_gostPlacable) _onPlacableSelect.Raise(false);
        _gostPlacable = Instantiate(_placableList[index]);
    }

    private void Place()
    {
        if (!_gostPlacable) return;
        if (_gostPlacable && _gostPlacable.CanBePlaced)
        {
            _ressourceCondition.Value -= _gostPlacable.cost.Value;
            Instantiate(_gostPlacable.PrefabToPlace, _gostPlacable.transform.position, _gostPlacable.transform.rotation);
            UnSelect();
        }
    }

    private void UnSelect()
    {
        Destroy(_gostPlacable.gameObject);
        _gostPlacable = null;
        _onPlacableSelect.Raise(true);
    }

    private void Update()
    {
        if (_gostPlacable)
        {
            _gostPlacable.transform.position = MouseAimPosition(_gostPlacable.transform.position);
        }
    }

    private void OnPlacePlacable(InputValue value)
    {
        Place();
    }

    private Vector3 MouseAimPosition(Vector3 currentPos)
    {
        Vector2 pixelPos = Input.mousePosition;
        Ray camRay = _mainCamera.ScreenPointToRay(pixelPos);

        if (DEBUG) Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.green);

        Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity, _groundLayer);
        if (!hit.collider) return currentPos;

        if (DEBUG) Debug.DrawLine(new Vector3(hit.point.x, hit.point.y - 1, hit.point.z)
                                , new Vector3(hit.point.x, hit.point.y + 1, hit.point.z)
                                , Color.red);

        return Vector3.Distance(transform.position, hit.point) > _range.Value ?
        transform.position + (hit.point - transform.position).normalized * _range.Value : hit.point;
    }
}
