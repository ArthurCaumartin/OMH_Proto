using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //TODO PLAYER Faire un script player Aim / player movement !!!
    public bool DEBUG = false;
    [Header("Movement :")]
    [SerializeField] private FloatReference _moveSpeed;
    [SerializeField] private FloatReference _moveAcceleration;
    [SerializeField] private List<FloatVariable> _externalMoveSpeedMult;

    private InputAction _moveInputAction;
    private Rigidbody _rb;
    private Vector2 _inputVector;
    private Vector3 _velocityTarget;
    public float _externalMoveSpeed;

    private void Start()
    {
        if (!GetComponent<PlayerInput>() || !GetComponent<PlayerInput>().actions)
        {
            print("NOT PLAYER INPUT ON PLAYER OBJECT");
            enabled = false;
            return;
        }

        _moveInputAction = GetComponent<PlayerInput>().actions.FindAction("GroundMove");
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _externalMoveSpeed = ComputeExternalMultiplier();

        _inputVector = _moveInputAction.ReadValue<Vector2>();
        _velocityTarget = new Vector3(_inputVector.x, 0, _inputVector.y)
                         * _moveSpeed.Value * _externalMoveSpeed
                         * 100 * Time.fixedDeltaTime;

        _rb.velocity = Vector3.Lerp(_rb.velocity, _velocityTarget, Time.deltaTime * _moveAcceleration.Value);
    }

    private float ComputeExternalMultiplier()
    {
        if(_externalMoveSpeedMult.Count == 0) return 1;

        //TODO fonctione mais manque de precision, plus y'a de valeur plus elle seront gommé :/
        float externalMultiplier = 0;
        for (int i = 0; i < _externalMoveSpeedMult.Count; i++)
            externalMultiplier += _externalMoveSpeedMult[i].Value;
        externalMultiplier = Mathf.Clamp(externalMultiplier / _externalMoveSpeedMult.Count, 0, 100);
        return externalMultiplier;
    }

    private void OnDisable()
    {
        _velocityTarget = Vector3.zero;
        _rb.velocity = Vector3.zero;
    }
}
