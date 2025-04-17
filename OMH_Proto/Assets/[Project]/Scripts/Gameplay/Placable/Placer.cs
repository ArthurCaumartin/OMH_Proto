using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [SerializeField] private GameEvent _onPlacableSelect, _onShowGrid, _onShowRails;
    [SerializeField] private Image _button1Image, _button2Image, _button3Image;
    private Placable _ghostPlacable;
    private Camera _mainCamera;
    private PlacerRail _railUnderMouse;
    private UnityEvent<GameObject> _onPlacePrefab = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnPlacePrefab { get => _onPlacePrefab; }

    private int _oldPlacableIndex;

    private void Start()
    {
        _mainCamera = Camera.main;
        _oldPlacableIndex = -1;
    }

    public void Select(int index)
    {
        if (!_levelGrid)
        {
            Debug.LogWarning("LE PLACER A PAS LA REF DE LA GRID DU LEVEL !!!");
            return;
        }

        // if (_ressourceCondition)
        // {
        //     if (_ressourceCondition.Value - _placableList[index].cost.Value < 0)
        //     {
        //         return;
        //     }
        // }

        //* If Player selecte a placable allready select
        if (_oldPlacableIndex == index)
        {
            if (!_ghostPlacable) _oldPlacableIndex = -1;
            UnSelect();
        }
        else
        {
            if (!_ghostPlacable) _onPlacableSelect.Raise(false);
            _ghostPlacable = Instantiate(_placableList[index]);
            _oldPlacableIndex = index;
        }
    }

    private void UnSelect()
    {
        _onShowGrid.Raise(false);
        _onShowRails.Raise(false);
        
        if (!_ghostPlacable) return;
        _ghostPlacable.ClearPlacable();
        Destroy(_ghostPlacable.gameObject);
        _ghostPlacable = null;
        _onPlacableSelect.Raise(true);
    }

    private void Place()
    {
        if (!_ghostPlacable) return;
        if (_ghostPlacable && _ghostPlacable.CanBePlaced)
        {
            if (_ghostPlacable.placeOnCorridorRail && !_railUnderMouse) return;
            if (_ressourceCondition) _ressourceCondition.Value -= _ghostPlacable.cost.Value;

            InstantiatePlaceblePrefab();
            UnSelect();
            _oldPlacableIndex = -1;
        }
    }

    private void InstantiatePlaceblePrefab()
    {
        GameObject newPrefab;
        if (_ghostPlacable.placeOnCorridorRail && _railUnderMouse)
        {
            newPrefab = Instantiate(_ghostPlacable.PrefabToPlace //! l'enchainement de converstion (dsl le moi du future)
                        , _railUnderMouse.GetNearestPosition(MouseAimPosition(_ghostPlacable.transform.position))
                        , _ghostPlacable.transform.rotation);
            return;
        }

        newPrefab = Instantiate(_ghostPlacable.PrefabToPlace
                                , WorldToCellConvert(MouseAimPosition(_ghostPlacable.transform.position))
                                , _ghostPlacable.transform.rotation);
        _onPlacePrefab.Invoke(newPrefab);
    }

    private void Update()
    {
        if (!_ghostPlacable) return;

        _railUnderMouse = CheckForRail();
        MoveGostPlacableToMouse();
    }

    private void MoveGostPlacableToMouse()
    {
        _ghostPlacable.IsOnRail = _railUnderMouse;
        if (_ghostPlacable.placeOnCorridorRail)
        {
            if (_railUnderMouse)
            {
                Vector3 railPosition = _railUnderMouse.GetNearestPosition(MouseAimPosition(_ghostPlacable.transform.position));
                _ghostPlacable.transform.forward = _railUnderMouse.GetDirection();
                _ghostPlacable.transform.position = railPosition;
                return;
            }
        }

        Vector3 invPlayerDir = _ghostPlacable.transform.position - _playerTransform.position;
        invPlayerDir.y = 0;

        invPlayerDir = invPlayerDir.normalized;
        invPlayerDir.x = Mathf.Round(invPlayerDir.x);
        invPlayerDir.z = Mathf.Round(invPlayerDir.z);
        if(invPlayerDir.x != 0) invPlayerDir.z = 0;

        _ghostPlacable.transform.forward = invPlayerDir;
        _ghostPlacable.transform.position = WorldToCellConvert(MouseAimPosition(_ghostPlacable.transform.position));
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
        _oldPlacableIndex = -1;
    }

    public void OnSelectPlacable1()
    {
        if (_ressourceCondition.Value - _placableList[2].cost.Value < 0)
        {
            StartCoroutine(NotEnoughMaterials(2));
            return;
        }
        
        UnSelect();
        _onShowRails.Raise();
        Select(2);
    }
    public void OnSelectPlacable2()
    {
        if (_ressourceCondition.Value - _placableList[0].cost.Value < 0)
        {
            StartCoroutine(NotEnoughMaterials(0));
            return;
        }
        
        UnSelect();
        _onShowGrid.Raise();
        Select(0);
    }
    public void OnSelectPlacable3()
    {
        if (_ressourceCondition.Value - _placableList[1].cost.Value < 0)
        {
            StartCoroutine(NotEnoughMaterials(1));
            return;
        }
        
        UnSelect();
        _onShowGrid.Raise();
        Select(1);
    }

    private IEnumerator NotEnoughMaterials(int index)
    {
        if(index == 0) _button1Image.color = Color.red;
        if(index == 1) _button2Image.color = Color.red;
        if(index == 2) _button3Image.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        if(index == 0) _button1Image.color = Color.white;
        if(index == 1) _button2Image.color = Color.white;
        if(index == 2) _button3Image.color = Color.white;
    }
}
