using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! tuto source : https://www.youtube.com/watch?v=jidloC6gyf8&ab_channel=DanielIlett

public class ThroughWallShaderControler : MonoBehaviour
{
    public bool DEBUG = false;
    [SerializeField, Range(0, 1)] private float _startCutSize = 0.2f;
    [SerializeField] private float _followSpeed = 15;
    [SerializeField] private float _sizeSpeed = 5;
    [Space]
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private Material _mat;
    private Camera _mainCamera;
    private float _startSize;
    private Vector3 _normaliseScreenPos;
    private float _cameraDistance;
    private bool _isLockOpen;

    private void Start()
    {
        _mainCamera = Camera.main;
        _mat.SetFloat("_CutSize", _startCutSize);
        _startSize = _startCutSize;
    }

    private void LateUpdate()
    {
        _normaliseScreenPos = _mainCamera.WorldToViewportPoint(transform.position + Vector3.up);
        _cameraDistance = Vector3.Distance(transform.position, _mainCamera.transform.position);

        RaycastHit[] hits = Physics.RaycastAll(_mainCamera.transform.position
                                            , (transform.position - _mainCamera.transform.position).normalized
                                            , _cameraDistance
                                            , _wallLayer);

        if (_isLockOpen)
            SetShaderParametre(_normaliseScreenPos, true);
        else
            SetShaderParametre(_normaliseScreenPos, hits.Length != 0);

        Debug();
    }

    private void Debug()
    {
        // print(hits.Length == 0 ? "Nothing hit !" : "it wall");
        if (DEBUG) UnityEngine.Debug.DrawRay(_mainCamera.transform.position
                                , (transform.position - _mainCamera.transform.position).normalized * _cameraDistance
                                , Color.cyan);
    }

    private void SetShaderParametre(Vector2 normaliseScreenPos, bool isOpen)
    {
        float target = isOpen ? _startSize : 0;
        _mat.SetVector("_CutPosition", Vector2.Lerp(_mat.GetVector("_CutPosition"), normaliseScreenPos, Time.deltaTime * _followSpeed));
        _mat.SetFloat("_CutSize", Mathf.Lerp(_mat.GetFloat("_CutSize"), target, Time.deltaTime * _sizeSpeed));
    }

    public void LockShaderOpen()
    {
        _isLockOpen = true;
    }

    public void UnlockShader()
    {
        _isLockOpen = false;
    }
}
