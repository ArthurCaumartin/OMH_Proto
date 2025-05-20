using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Seller : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField] private FloatVariable _metalQuantity;
    [SerializeField] private Transform _sellerSelectorVisualPrefab;
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Color _notHoverColor;
    [Space]
    [SerializeField] private FloatReference _range;
    [SerializeField] private float _speed;
    [Space]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Placer _placer;
    [SerializeField] private LayerMask _aimLayer;
    [SerializeField] private GameEvent _onPlacableSelect;
    [SerializeField] private GameEvent _onShowGrid;
    [SerializeField] private GameEvent _onShowRails;
    [SerializeField] private GameEvent _showTrap;

    private Transform _selectorVisual;
    private bool _isEnable = true;
    private Camera _mainCamera;
    private Light _selectorLight;
    private Renderer _selectorRenderer;
    private Vector3 _worlMousePos;
    private CostBackup _nearestSellable;

    private void Start()
    {
        _mainCamera = Camera.main;
        _selectorVisual = Instantiate(_sellerSelectorVisualPrefab);

        _selectorLight = _selectorVisual.GetComponentInChildren<Light>();
        _selectorRenderer = _selectorVisual.GetComponentInChildren<MeshRenderer>();

        EnableSellMode(false);
    }

    public void EnableSellMode(bool value)
    {
        // print("Enable Sell Mode : " + value);
        _isEnable = value;
        _placer.UnSelect();
        _selectorVisual.gameObject.SetActive(value);

        _onShowGrid.Raise(value);
        // _onShowRails.Raise(value); //! hide rail and trap
        // _showTrap.Raise(value);
        _onPlacableSelect.Raise(!value);
    }

    private void Update()
    {
        if (!_isEnable) return;

        _nearestSellable = GetSellableItemNearMouse();

        // on peut plus tirer
        // on peut pas selectioner de defense a place 

        _selectorLight.color = _nearestSellable ? _hoverColor : _notHoverColor;
        _selectorRenderer.material.SetColor("_ObjectColor", _nearestSellable ? _hoverColor : _notHoverColor);

        _worlMousePos = MouseAimPosition(_selectorVisual.position);
        _selectorVisual.position = Vector3.Lerp(_selectorVisual.position, _nearestSellable ? _nearestSellable.transform.position : _worlMousePos, Time.deltaTime * _speed);

    }

    public void ToogleSeller()
    {
        EnableSellMode(!_isEnable);
    }

    private void SellCurrentCostBackup()
    {
        if (_nearestSellable)
        {
            // _metalQuantity.Value += _nearestSellable.GetCostOnHealth();
            _metalQuantity.Add(_nearestSellable.GetCostOnHealth());
            _nearestSellable.GetComponentInChildren<DisolveEffect>().Disolve(true, true);
            Destroy(_nearestSellable.gameObject);
            EnableSellMode(false);
        }
    }

    private CostBackup GetSellableItemNearMouse()
    {
        List<RaycastHit> hits = Physics.SphereCastAll(_worlMousePos, 3, Vector3.up).ToList();
        hits.RemoveAll(item => !item.collider.GetComponent<CostBackup>());

        float minDistance = Mathf.Infinity;
        CostBackup toReturn = null;

        for (int i = 0; i < hits.Count; i++)
        {
            float currentDistance = Vector3.Distance(_worlMousePos, hits[i].transform.position);
            if (minDistance > currentDistance)
            {
                minDistance = currentDistance;
                toReturn = hits[i].collider.GetComponent<CostBackup>();
            }
        }

        // print("BacupToReturn : " + toReturn?.name);
        return toReturn;
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

    private void OnDeselectPlacable(InputValue value)
    {
        EnableSellMode(false);
    }

    private void OnPlacePlacable(InputValue input)
    {
        if(!_isEnable) return;
        SellCurrentCostBackup();
    }

    private void OnSelectSellMode(InputValue value)
    {
        if (value.Get<float>() > .5)
            EnableSellMode(true);
    }
}