using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Placer : MonoBehaviour
{
    public bool DEBUG = true;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Grid _levelGrid;
    [SerializeField] private LayerMask _aimLayer;
    [SerializeField] private FloatReference _range;
    [Space]
    [SerializeField] private FloatVariable _ressourceCondition;
    [Space]
    [SerializeField] private List<Placable> _placableList;
    [SerializeField] private GameEvent _onPlacableSelect;
    private Placable _gostPlacable;
    private Camera _mainCamera;
    private PlacerRail _railUnderMouse;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void Select(int index)
    {
        if (!_levelGrid)
        {
            Debug.LogWarning("LE PLACER A PAS LA REF DE LA GRID DU LEVEL !!!");
            return;
        }

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

    private void UnSelect()
    {
        if(!_gostPlacable) return;
        Destroy(_gostPlacable.gameObject);
        _gostPlacable = null;
        _onPlacableSelect.Raise(true);
    }

    private void Place()
    {
        if (!_gostPlacable) return;
        if (_gostPlacable && _gostPlacable.CanBePlaced)
        {
            if (_gostPlacable.placeOnCorridorRail && !_railUnderMouse) return;
            if (_ressourceCondition) _ressourceCondition.Value -= _gostPlacable.cost.Value;

            InstantiatePlaceblePrefab();
            UnSelect();
        }
    }

    private void InstantiatePlaceblePrefab()
    {

        if (_gostPlacable.placeOnCorridorRail && _railUnderMouse)
        {
            Instantiate(_gostPlacable.PrefabToPlace //! l'enchainement de converstion (dsl le moi du future)
                        , _railUnderMouse.GetNearestPosition(MouseAimPosition(_gostPlacable.transform.position))
                        , _gostPlacable.transform.rotation);
            return;
        }

        Instantiate(_gostPlacable.PrefabToPlace, WorldToCellConvert(MouseAimPosition(_gostPlacable.transform.position)), _gostPlacable.transform.rotation);
    }

    private void Update()
    {
        if (!_gostPlacable) return;

        _railUnderMouse = CheckForRail();
        MoveGostPlacableToMouse();
    }

    private void MoveGostPlacableToMouse()
    {
        if (_gostPlacable.placeOnCorridorRail)
        {
            if (_railUnderMouse)
            {
                Vector3 railPosition = _railUnderMouse.GetNearestPosition(MouseAimPosition(_gostPlacable.transform.position));
                _gostPlacable.transform.forward = _railUnderMouse.GetDirection();
                _gostPlacable.transform.position = Vector3.Lerp(_gostPlacable.transform.position
                                                                , railPosition
                                                                , Time.deltaTime * 10);
                return;
            }
        }

        _gostPlacable.transform.position = Vector3.Lerp(_gostPlacable.transform.position
                                                        , WorldToCellConvert(MouseAimPosition(_gostPlacable.transform.position))
                                                        , Time.deltaTime * 10);
    }


    private Vector3 MouseAimPosition(Vector3 currentPos)
    {
        Vector2 pixelPos = Input.mousePosition;
        Ray camRay = _mainCamera.ScreenPointToRay(pixelPos);


        Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity, _aimLayer);
        if (!hit.collider) return currentPos;

        if (DEBUG) Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.green);
        if (DEBUG) Debug.DrawLine(new Vector3(hit.point.x, hit.point.y - 1, hit.point.z)
                                , new Vector3(hit.point.x, hit.point.y + 1, hit.point.z)
                                , Color.red);

        return Vector3.Distance(_playerTransform.position, hit.point) > _range.Value ?
        _playerTransform.position + (hit.point - _playerTransform.position).normalized * _range.Value : hit.point;
    }

    public PlacerRail CheckForRail()
    {
        Collider[] cols = Physics.OverlapSphere(MouseAimPosition(Vector3.one * 1000), 1f);
        for (int i = 0; i < cols.Length; i++)
        {
            PlacerRail p = cols[i].GetComponent<PlacerRail>();
            if (p) return p;
        }
        return null;
    }

    public Vector3 WorldToCellConvert(Vector3 worldPos, bool getCenterOfCell = true)
    {
        //! GetCellCenterWorld return .5 on y ???
        Vector3 cellCenter;
        if (getCenterOfCell)
            cellCenter = _levelGrid.GetCellCenterWorld(_levelGrid.WorldToCell(worldPos));
        else
            cellCenter = _levelGrid.WorldToCell(_levelGrid.WorldToCell(worldPos));

        return new Vector3(cellCenter.x, 0, cellCenter.z);
    }

    private void OnPlacePlacable(InputValue value)
    {
        Place();
    }

    private void OnDeselectPlacable(InputValue value)
    {
        UnSelect();
    }
}
