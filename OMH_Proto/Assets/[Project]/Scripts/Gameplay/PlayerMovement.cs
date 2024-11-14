using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //TODO PLAYER Faire un script player Aim / player movement !!!
    public bool DEBUG = false;
    [Header("Movement :")]
    [SerializeField] private FloatReference _moveSpeed;
    [SerializeField] private FloatReference _moveAcceleration;

    private InputAction _moveInputAction;
    private Rigidbody _rb;
    private Vector2 _inputVector;
    private Camera _mainCamera;

    private void Start()
    {
        if (!GetComponent<PlayerInput>() || !GetComponent<PlayerInput>().actions)
        {
            print("NOT PLAYER INPUT ON PLAYER OBJECT");
            enabled = false;
            return;
        }

        _moveInputAction = GetComponent<PlayerInput>().actions.FindAction("Move");
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _inputVector = _moveInputAction.ReadValue<Vector2>();
        Vector3 velocityTarget = new Vector3(_inputVector.x, 0, _inputVector.y) * _moveSpeed.Value * 100 * Time.fixedDeltaTime;
        _rb.velocity = Vector3.Lerp(_rb.velocity, velocityTarget, Time.deltaTime * _moveAcceleration.Value);
    }
}
