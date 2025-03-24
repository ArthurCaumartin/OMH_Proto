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
    [SerializeField] private float _transitionSpeed = 5;
    private Renderer[] _rendererArray;
    private Transform _cameraTransform;
    private bool _isBehind = false;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _rendererArray = GetComponentsInChildren<Renderer>();

        _health?.SetDelayBeforDestroy(_disolveDuration);
        _health?.OnDeathEvent.AddListener(() => Disolve(true));

        Disolve(_initialVisivbility);
    }

    void Update()
    {
        if (!_objectToSeeThrought) return;

        Vector3 dir = (_cameraTransform.position - _objectToSeeThrought.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(_objectToSeeThrought.position, dir, 10);

        _isBehind = false;
        foreach (var item in hits)
        {
            if (item.collider.gameObject == gameObject)
            {
                _isBehind = true;
                break;
            }
        }

        float current = _rendererArray[0].material.GetFloat(_parameterName);
        SetMaterialsParameter(Mathf.Lerp(current, _isBehind ? 0 : 1, Time.deltaTime * _transitionSpeed));
    }

    public void Disolve(bool isHiding)
    {
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
            item.material.SetFloat(_parameterName, time);
    }
}
