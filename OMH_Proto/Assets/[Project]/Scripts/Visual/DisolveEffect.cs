using System.Linq;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class DisolveEffect : MonoBehaviour
{
    [SerializeField] private string _parameterName = "_Alpha";
    [SerializeField] private float _disolveDuration = 1;
    [SerializeField] private bool _initialVisivbility = false;
    [Space]
    [SerializeField] private Health _health;
    [Space]
    [SerializeField] private Transform _objectToSeeThrought;
    [SerializeField, Range(0, 1)] private float _disolveTresold = 5;
    [SerializeField] private float _transitionSpeed = 5;
    private Renderer[] _rendererArray;
    private Transform _cameraTransform;
    private float _dotValue;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _rendererArray = GetComponentsInChildren<Renderer>();

        _health?.OnDeathEvent.AddListener(() => Disolve(true, true));

        Disolve(_initialVisivbility);
    }

    void Update()
    {
        if (!_objectToSeeThrought) return;

        Vector3 dirToObj = (_objectToSeeThrought.position - transform.position).normalized;
        _dotValue = Vector3.Dot(_cameraTransform.forward, dirToObj);
        float current = _rendererArray[0].material.GetFloat(_parameterName);
        SetMaterialsParameter(Mathf.Lerp(current, _dotValue > _disolveTresold ? 0 : 1, Time.deltaTime * _transitionSpeed));
    }

    public void Disolve(bool isHiding, bool isoleMesh = false)
    {
        if(isoleMesh)
        {
            transform.parent = null;
            Destroy(gameObject, _disolveDuration);
        }
        // print("ksedfrgbj");
        SetMaterialsParameter(isHiding ? 1 : 0);
        DOTween.To((time) =>
        {
            SetMaterialsParameter(time);
        }, isHiding ? 1 : 0, isHiding ? 0 : 1, _disolveDuration);
    }

    private void SetMaterialsParameter(float time)
    {
        foreach (var item in _rendererArray)
            foreach (var mat in item.materials)
                mat.SetFloat(_parameterName, time);
    }
}
