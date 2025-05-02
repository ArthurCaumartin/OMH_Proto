using UnityEngine;

public class Seller : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField] private Transform _sellerSelectorVisualPrefab;
    [SerializeField] private FloatReference _range;
    [Space]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Placer _placer;
    [SerializeField] private LayerMask _aimLayer;
    private Transform _selectorVisual;
    private GameObject _currentDefenceSelect;
    private bool _isEnable = true;
    private Camera _mainCamera;

    private void Start()
    {
        _selectorVisual = Instantiate(_sellerSelectorVisualPrefab);
        EnableSellMode(false);
    }

    public void EnableSellMode(bool value)
    {
        _isEnable = value;
        _placer.UnSelect();
        _selectorVisual.gameObject.SetActive(value);

    }

    private void Update()
    {
        if (!_isEnable) return;

        _selectorVisual.position = MouseAimPosition(_selectorVisual.position);
    }

    public void ToogleSeller()
    {
        EnableSellMode(!_isEnable);
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
}