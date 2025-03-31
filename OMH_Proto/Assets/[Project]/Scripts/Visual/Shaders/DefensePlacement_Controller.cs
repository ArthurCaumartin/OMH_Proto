using UnityEngine;

public class DefensePlacement_Controller : MonoBehaviour
{
    [SerializeField] private Transform _renderer;
    [SerializeField] private LayerMask _aimLayer;
    public Camera _mainCamera;
    private Vector3 _currentMousePos = Vector3.zero;
    private Vector3 _localMousePos = Vector3.zero;
    private Vector2 _normalizePos = Vector2.zero;

    public Material _GridMaterial;

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    float scale;
    private void Update()
    {
        _currentMousePos = MouseAimPosition(_currentMousePos);

        _localMousePos = transform.InverseTransformPoint(_currentMousePos);

        scale = _renderer.localScale.x / 2;
        // Debug.DrawRay(transform.TransformPoint(new Vector3(scale, 0, scale)), Vector3.up, Color.green);
        // Debug.DrawRay(transform.TransformPoint(new Vector3(-scale, 0, -scale)), Vector3.up, Color.green);

        _normalizePos.x = Mathf.InverseLerp(-scale, scale, _localMousePos.x) - .5f;
        _normalizePos.y = Mathf.InverseLerp(-scale, scale, _localMousePos.z) - .5f;

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
        return hit.point;
    }
}
