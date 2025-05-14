using System.Collections.Generic;
using UnityEngine;

public class BarriereDefense_Controler : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private string _emissiveParametreName;
    [SerializeField] private List<Renderer> _planeRenderer;

    [Header("Emissive : ")]
    [SerializeField] private float _emissiveSpeed = 120;
    [SerializeField] private float _emissiveTarget = 40;
    [SerializeField] private float _emissiveMax = 120;
    [SerializeField] private float _emissiveMin = 40;
    [SerializeField] private float _emissiveToAddOnDamage = 120;

    [Header("Opacity : ")]
    [SerializeField] private string _opacityParameterName;
    [SerializeField] private float _opacityMax = 5;
    [SerializeField] private float _opacityMin = 0;
    [SerializeField] private float _opacitySpeed = 5;
    private float _currentOpacity;

    private void Start()
    {
        _health.OnDamageTaken.AddListener((notUse) => OnDamageTaken());
    }

    private void Update()
    {
        UpdateEmissive();
        UpdateOpacity();
    }

    private void UpdateEmissive()
    {
        foreach (var item in _planeRenderer)
            item.material.SetFloat(_emissiveParametreName, _emissiveTarget);

        _emissiveTarget -= Time.deltaTime * _emissiveSpeed;
        _emissiveTarget = Mathf.Clamp(_emissiveTarget, _emissiveMin, _emissiveMax);
    }

    private void OnDamageTaken()
    {
        _emissiveTarget += _emissiveToAddOnDamage;
    }

    private void UpdateOpacity()
    {
        float target = Mathf.Lerp(_opacityMin, _opacityMax, _health.GetHealtRatio());
        _currentOpacity = Mathf.Lerp(_currentOpacity, target, Time.deltaTime * _opacitySpeed);
        foreach (var item in _planeRenderer)
            item.material.SetFloat(_opacityParameterName, _currentOpacity);
    }
}
