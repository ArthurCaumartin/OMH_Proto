using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField] private LayerMask _groundLayer = 10;
    [SerializeField] private Transform _aimContainer;
    [SerializeField] private FloatReference _aimSpeed;
    private Vector3 _mouseWorldPos;
    private Vector3 _worldMouseDirection;
    private CameraControler _camControler;
    private InputAction _aimInputAction;
    private Vector3 _aimVelocity;
    private Camera _mainCamera;

    public Vector3 WorldMouseDirection { get => _worldMouseDirection; }

    private void Start()
    {
        if (!GetComponent<PlayerInput>() || !GetComponent<PlayerInput>().actions)
        {
            print("NOT PLAYER INPUT ON PLAYER OBJECT");
            enabled = false;
            return;
        }

        _mainCamera = Camera.main;
        _camControler = _mainCamera.GetComponent<CameraControler>();
        _aimInputAction = GetComponent<PlayerInput>().actions.FindAction("Aim");
    }

    private void FixedUpdate()
    {
        MouseAim();
    }

    /// <summary> 
    /// Get la pos du pointer sur le sol et compute la direction par rapport au joueur
    /// A besoin d'avoir du sol pour upadate la world mouse pos
    /// <summary> 
    private void MouseAim()
    {
        Vector2 pixelPos = _aimInputAction.ReadValue<Vector2>();
        Ray camRay = _mainCamera.ScreenPointToRay(pixelPos);

        if (DEBUG) Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.green);

        Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity, _groundLayer);
        if (!hit.collider) return;

        if (DEBUG) Debug.DrawLine(new Vector3(hit.point.x, hit.point.y - 1, hit.point.z)
                                , new Vector3(hit.point.x, hit.point.y + 1, hit.point.z)
                                , Color.red);

        _mouseWorldPos = Vector3.SmoothDamp(_mouseWorldPos, hit.point, ref _aimVelocity, 1 / _aimSpeed.Value, Mathf.Infinity);
        _worldMouseDirection = (_mouseWorldPos - transform.position).normalized;
        _worldMouseDirection.y = 0;

        //TODO integrer la distance joueur / _mouseWorldPos 
        _camControler?.SetInputOffSet(_worldMouseDirection);
        _aimContainer.forward = _worldMouseDirection;
    }

    // private void ControlerAim()
    // {
    //     if (!_canAim) return;

    //     Vector2 aimInput = _aimInputAction.ReadValue<Vector2>();
    //     if (aimInput == Vector2.zero) return;
    //     _currentAimTarget = Vector2.SmoothDamp(_currentAimTarget, aimInput, ref _aimVelocity, 1 / _aimSpeed, Mathf.Infinity);
    //     _camControler.SetInputOffSet(_currentAimTarget);
    //     _aimContainer.forward = new Vector3(_currentAimTarget.x, 0, _currentAimTarget.y);
    // }

    public Vector3 GetAimDirection()
    {
        return _aimContainer.forward;
    }
}
