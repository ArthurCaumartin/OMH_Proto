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

    private void Start()
    {
        _mainCamera = Camera.main;
        _mat.SetFloat("_CutSize", _startCutSize);
        _startSize = _startCutSize;
    }

    private void LateUpdate()
    {
        //TODO fix la distance du raycast :)
        Vector2 normaliseScreenPos = _mainCamera.WorldToViewportPoint(transform.position);
        // _mat.SetVector("_CutPosition", normaliseScreenPos);


        if (DEBUG) Debug.DrawRay(_mainCamera.transform.position, (transform.position - _mainCamera.transform.position).normalized * 100, Color.cyan);
        RaycastHit[] hits = Physics.RaycastAll(_mainCamera.transform.position
                                        , (transform.position - _mainCamera.transform.position).normalized, 100, _wallLayer);

        // print(hits.Length == 0 ? "Nothing hit !" : "it wall");
        float target = hits.Length != 0 ? _startSize : 0;
        _mat.SetVector("_CutPosition", Vector2.Lerp(_mat.GetVector("_CutPosition"), normaliseScreenPos, Time.deltaTime * _followSpeed));
        _mat.SetFloat("_CutSize", Mathf.Lerp(_mat.GetFloat("_CutSize"), target, Time.deltaTime * _sizeSpeed));
    }
}
