using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class OutlineControler : MonoBehaviour
{
    public static List<OutlineControler> _controlerList = new List<OutlineControler>();
    [SerializeField] private Material _materialToSetOnRenderer;
    [SerializeField] private string _parameterName;
    [SerializeField] private float _thickness = 1.5f;
    [SerializeField] private float _animationDuration = 1.5f;
    [SerializeField] private AnimationCurve _outlineAnimationCurve;
    private MeshRenderer[] _meshRendererList;
    private float _currentThicness = -1;

    void Awake()
    {
        if (!_controlerList.Contains(this))
            _controlerList.Add(this);
    }

    private void Start()
    {
        _controlerList.RemoveAll(item => item == null);
        _meshRendererList = GetComponentsInChildren<MeshRenderer>();

        SetThickness(0);
    }

    public void ShowOutline(bool value)
    {
        SetThickness(value ? _thickness : 0);
    }

    public void SetThickness(float thicknessTarget)
    {
        if (_currentThicness == thicknessTarget) return;
        _currentThicness = thicknessTarget;

        foreach (var renderer in _meshRendererList)
        {
            foreach (var material in renderer.materials)
            {
                if (material.name.Split(" ")[0] == _materialToSetOnRenderer.name)
                {
                    float startValue = material.GetFloat(_parameterName);
                    DOTween.To((time) =>
                    {
                        material.SetFloat(_parameterName, _currentThicness);
                    }, startValue, thicknessTarget, _animationDuration)
                    .SetEase(_outlineAnimationCurve);
                }
            }
        }
    }
}